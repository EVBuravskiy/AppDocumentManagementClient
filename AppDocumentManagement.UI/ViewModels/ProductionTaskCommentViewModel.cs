using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Services;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppDocumentManagement.UI.ViewModels
{
    public class ProductionTaskCommentViewModel : BaseViewModelClass
    {
        private ProductionTaskCommentWindow ProductionTaskCommentWindow { get; set; }
        private ProductionTask CurrentProductionTask { get; set; }

        private Employee CurrentEmployee { get; set; }

        public List<ProductionTaskComment> ProductionTaskCommentsList { get; set; }
        public ObservableCollection<ProductionTaskComment> ProductionTaskComments { get; set; }

        private string productionTaskCommentText = string.Empty;

        public string ProductionTaskCommentText
        {
            get => productionTaskCommentText;
            set
            {
                productionTaskCommentText = value;
                OnPropertyChanged(nameof(ProductionTaskCommentText));
            }
        }
        public ProductionTaskCommentViewModel(ProductionTaskCommentWindow productionTaskCommentWindow, ProductionTask currentProductionTask, Employee currentEmployee)
        {
            ProductionTaskCommentWindow = productionTaskCommentWindow;
            CurrentProductionTask = currentProductionTask;
            CurrentEmployee = currentEmployee;
            GetPhotoCurrentEmployee();
            ProductionTaskCommentsList = new List<ProductionTaskComment>();
            ProductionTaskComments = new ObservableCollection<ProductionTaskComment>();
            GetAllProductionTaskComments();
            InitializeProductTaskComments();
        }

        private void GetPhotoCurrentEmployee()
        {
            if (CurrentEmployee != null)
            {
                EmployeePhotoService employeePhotoService = new EmployeePhotoService();
                EmployeePhoto photo = employeePhotoService.GetEmployeePhotoByEmployeeID(CurrentEmployee.EmployeeID).Result;
                if (photo != null)
                {
                    string photoPath = FileProcessing.SaveEmployeePhotoToTempFolder(photo);
                    photo.FilePath = photoPath;
                    CurrentEmployee.EmployeePhoto = photo;
                }
            }
        }

        private void GetAllProductionTaskComments()
        {
            ProductionTaskCommentsList.Clear();
            if (CurrentProductionTask != null)
            {
                ProductionTaskCommentService productionTaskCommentService = new ProductionTaskCommentService();
                ProductionTaskCommentsList = productionTaskCommentService.GetProductionTaskComments(CurrentProductionTask.ProductionTaskID).Result;
                if (ProductionTaskCommentsList.Count > 0)
                {
                    foreach (ProductionTaskComment productionTaskComment in ProductionTaskCommentsList)
                    {
                        EmployesService employesService = new EmployesService();
                        Employee employee = employesService.GetEmployeeByID(productionTaskComment.EmployeeID).Result;
                        if (employee != null)
                        {
                            EmployeePhotoService employeePhotoService = new EmployeePhotoService();
                            EmployeePhoto photo = employeePhotoService.GetEmployeePhotoByEmployeeID(employee.EmployeeID).Result;
                            if (photo != null)
                            {
                                string photoPath = FileProcessing.SaveEmployeePhotoToTempFolder(photo);
                                photo.FilePath = photoPath;
                                employee.EmployeePhoto = photo;
                            }
                        }
                        productionTaskComment.Employee = employee;
                    }
                }
            }
        }

        private void InitializeProductTaskComments()
        {
            ProductionTaskComments.Clear();
            if (ProductionTaskCommentsList.Count > 0)
            {
                foreach (ProductionTaskComment comment in ProductionTaskCommentsList)
                {
                    ProductionTaskComments.Add(comment);
                }
            }
        }

        public ICommand IAddProductionTaskComment => new RelayCommand(addProductionTaskComment => AddProductionTaskComment());
        private void AddProductionTaskComment()
        {
            if (string.IsNullOrEmpty(ProductionTaskCommentText) || string.IsNullOrWhiteSpace(ProductionTaskCommentText)) return;
            ProductionTaskComment productionTaskComment = new ProductionTaskComment();
            productionTaskComment.ProductionTaskCommentText = ProductionTaskCommentText.Trim();
            productionTaskComment.ProductionTaskCommentDate = DateTime.Now;
            productionTaskComment.EmployeeID = CurrentEmployee.EmployeeID;
            productionTaskComment.Employee = CurrentEmployee;
            if (CurrentProductionTask != null && CurrentProductionTask.ProductionTaskID != 0)
            {
                productionTaskComment.ProductionTask = CurrentProductionTask;
                productionTaskComment.ProductionTaskID = CurrentProductionTask.ProductionTaskID;
            }
            ProductionTaskCommentsList.Add(productionTaskComment);
            ProductionTaskCommentText = string.Empty;
            InitializeProductTaskComments();
        }

        public ICommand IExit => new RelayCommand(exit => { ProductionTaskCommentWindow.Close(); });
    }
}
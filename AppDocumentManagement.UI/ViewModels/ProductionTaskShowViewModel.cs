using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.ExternalDocumentService.Services;
using AppDocumentManagement.InternalDocumentService.Services;
using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Services;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagement.UI.ViewModels
{
    public class ProductionTaskShowViewModel : BaseViewModelClass
    {
        private ProductionTaskShowWindow ProductionTaskShowWindow { get; set; }
        private Employee CurrentEmployee { get; set; }
        private ProductionTask CurrentProductionTask { get; set; }

        private IFileDialogService fileDialogService;

        private ExternalDocument ExternalDocument { get; set; }
        private InternalDocument InternalDocument { get; set; }

        private string productionTaskTitle = string.Empty;

        public string ProductionTaskTitle
        {
            get => productionTaskTitle;
            set
            {
                productionTaskTitle = value;
                OnPropertyChanged(nameof(ProductionTaskTitle));
            }
        }

        private string documentTitle = string.Empty;

        public string DocumentTitle
        {
            get => documentTitle;
            set
            {
                documentTitle = value;
                OnPropertyChanged(nameof(DocumentTitle));
            }
        }

        private DateTime productionTaskDueDate;
        public DateTime ProductionTaskDueDate
        {
            get => productionTaskDueDate;
            set
            {
                productionTaskDueDate = value;
                OnPropertyChanged(nameof(ProductionTaskDueDate));
            }
        }

        public bool IsImportance { get; set; }

        private string productionTaskDescription = string.Empty;

        public string ProductionTaskDescription
        {
            get => productionTaskDescription;
            set
            {
                productionTaskDescription = value;
                OnPropertyChanged(nameof(ProductionTaskDescription));
            }
        }

        private bool isImportant = false;
        public bool IsImportant
        {
            get => isImportant;
            set
            {
                isImportant = value;
                OnPropertyChanged(nameof(IsImportant));
            }
        }
        private List<ProductionSubTask> ProductionSubTasksList { get; set; }
        public ObservableCollection<ProductionSubTask> ProductionSubTasks { get; set; }

        public ObservableCollection<Employee> ProductionTaskPerformers { get; set; }
        private List<ProductionTaskFile> ProductionTaskFilesList { get; set; }
        public ObservableCollection<ProductionTaskFile> ProductionTaskFiles { get; set; }

        private ProductionTaskFile selectedProductionTaskFile;
        public ProductionTaskFile SelectedProductionTaskFile
        {
            get => selectedProductionTaskFile;
            set
            {
                selectedProductionTaskFile = value;
                OnPropertyChanged(nameof(SelectedProductionTaskFile));
                if (value != null)
                {
                    BrowseToSaveProductionTaskFile();
                }
            }
        }

        private List<ProductionTaskComment> ProductionTaskComments { get; set; }

        public ProductionTaskShowViewModel(ProductionTaskShowWindow window, Employee currentEmployee, ProductionTask currentProductionTask)
        {
            ProductionTaskShowWindow = window;
            CurrentEmployee = currentEmployee;
            CurrentProductionTask = currentProductionTask;
            fileDialogService = new WindowsDialogService();
            if (currentProductionTask.ExternalDocumentID != 0)
            {
                ExternalDocumentsService externalDocumentsService = new ExternalDocumentsService();
                ExternalDocument = externalDocumentsService.GetExternalDocumentsByExternalDocumentID(currentProductionTask.ExternalDocumentID).Result;
                DocumentTitle = ExternalDocument.ExternalDocumentTitle;
                ProductionTaskShowWindow.ExternalDocumentBtn.Visibility = Visibility.Visible;
                ProductionTaskShowWindow.InternalDocumentBtn.Visibility = Visibility.Hidden;
            }
            if (currentProductionTask.InternalDocumentID != 0)
            {
                InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
                InternalDocument = internalDocumentsService.GetInternalDocumentsByInternalDocumentID(currentProductionTask.InternalDocumentID).Result;
                DocumentTitle = InternalDocument.InternalDocumentTitle;
                ProductionTaskShowWindow.InternalDocumentBtn.Visibility = Visibility.Visible;
                ProductionTaskShowWindow.ExternalDocumentBtn.Visibility = Visibility.Hidden;
            }
            if (DocumentTitle == string.Empty)
            {
                ProductionTaskShowWindow.DocumentInfo.Visibility = Visibility.Hidden;
            }
            if (CurrentProductionTask.ProductionTaskStatus != ProductionTaskStatus.InProgress)
            {
                ProductionTaskShowWindow.SendTask.Visibility = Visibility.Hidden;
                if (currentProductionTask.EmployeeCreatorID == currentEmployee.EmployeeID)
                {
                    ProductionTaskShowWindow.ConfirmTaskCompletion.Visibility = Visibility.Visible;
                }
            }
            if (CurrentProductionTask.ProductionTaskStatus == ProductionTaskStatus.Done)
            {
                ProductionTaskShowWindow.AcceptStatus.Visibility = Visibility.Hidden;
                ProductionTaskShowWindow.ListOfSubTask.IsEnabled = false;
                ProductionTaskShowWindow.ConfirmTaskCompletion.Visibility = Visibility.Hidden;
            }
            IsImportance = CurrentProductionTask.Priority;
            ProductionTaskTitle = CurrentProductionTask.ProductionTaskTitle;
            ProductionTaskDueDate = currentProductionTask.ProductionTaskDueDate;
            ProductionTaskDescription = currentProductionTask.ProductionTaskDescription ?? string.Empty;
            ProductionSubTasks = new ObservableCollection<ProductionSubTask>();
            GetProductionSubTasks();
            GetProductionTaskCreator();
            ProductionTaskPerformers = new ObservableCollection<Employee>();
            GetProductionTaskPerformers();
            ProductionTaskFilesList = new List<ProductionTaskFile>();
            GetProductionTaskFiles();
            ProductionTaskFiles = new ObservableCollection<ProductionTaskFile>();
            InitializeProductionTaskFiles();
        }

        private void GetProductionSubTasks()
        {
            ProductionSubTasks.Clear();
            ProductionTaskSubService productionTaskSubService = new ProductionTaskSubService();
            ProductionSubTasksList = productionTaskSubService.GetProductionSubTasks(CurrentProductionTask.ProductionTaskID).Result;
            CurrentProductionTask.ProductionSubTasks = ProductionSubTasksList;
            if (ProductionSubTasksList != null && ProductionSubTasksList.Count > 0)
            {
                foreach (ProductionSubTask productionSubTask in ProductionSubTasksList)
                {
                    ProductionSubTasks.Add(productionSubTask);
                }
            }
        }

        private void GetProductionTaskCreator()
        {
            if (CurrentProductionTask.EmployeeCreatorID != 0)
            {
                EmployesService employesService = new EmployesService();
                Employee creator = employesService.GetEmployeeByID(CurrentProductionTask.EmployeeCreatorID).Result;
                if (creator != null)
                {
                    CurrentProductionTask.EmployeeCreator = creator;
                }
            }
        }

        private void GetProductionTaskPerformers()
        {
            ProductionTaskPerformers.Clear();
            if (CurrentProductionTask.Employees.Count > 0)
            {
                foreach (Employee performer in CurrentProductionTask.Employees)
                {
                    ProductionTaskPerformers.Add(performer);
                }
            }
        }
        private void GetProductionTaskFiles()
        {
            ProductionTaskFilesList.Clear();
            ProductionTaskFileService productionTaskFileService = new ProductionTaskFileService();
            ProductionTaskFilesList = productionTaskFileService.GetProductionTaskFiles(CurrentProductionTask.ProductionTaskID).Result;
        }

        private void InitializeProductionTaskFiles()
        {
            ProductionTaskFiles.Clear();
            if (ProductionTaskFilesList.Count > 0)
            {
                foreach (ProductionTaskFile productionTaskFile in ProductionTaskFilesList)
                {
                    ProductionTaskFiles.Add(productionTaskFile);
                }
            }
        }

        public ICommand ILoadProductionTaskFiles => new RelayCommand(loadProductionTaskFiles => LoadProductionTaskFiles());
        private void LoadProductionTaskFiles()
        {
            string directoryPath = string.Empty;
            foreach (ProductionTaskFile file in ProductionTaskFiles)
            {
                directoryPath = FileProcessing.SaveProductionTaskFileFromDB(file, "Tasks");
            }
            if (string.IsNullOrEmpty(directoryPath))
            {
                MessageBox.Show("Не удалось сохранить файлы");
            }
            else
            {
                DirectoryProcessing.OpenDirectory(directoryPath);
            }
        }

        public ICommand IBrowseToSaveProductionTaskFile => new RelayCommand(browseToSaveProductionTaskFile => BrowseToSaveProductionTaskFile());
        private void BrowseToSaveProductionTaskFile()
        {
            var filePath = fileDialogService.SaveFile(SelectedProductionTaskFile.ProductionTaskFileExtension, SelectedProductionTaskFile.ProductionTaskFileName);
            bool result = FileProcessing.SaveProductionTaskFileToPath(filePath, SelectedProductionTaskFile);
            if (result)
            {
                MessageBox.Show($"Файл {SelectedProductionTaskFile.ProductionTaskFileName} сохранен");
            }
            else
            {
                MessageBox.Show($"Файл {SelectedProductionTaskFile.ProductionTaskFileName} уже имеется, либо не был сохранен");
            }
        }

        public ICommand IBrowseProductionTaskFile => new RelayCommand(browseProductionTaskFile => BrowseProductionTaskFile());
        private void BrowseProductionTaskFile()
        {
            var filePath = fileDialogService.OpenFile();
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            ProductionTaskFile productionTaskFile = new ProductionTaskFile();
            productionTaskFile.ProductionTaskFileName = fileName;
            productionTaskFile.ProductionTaskFileExtension = fileExtension;
            productionTaskFile.ProductionTaskFileData = fileData;
            productionTaskFile.ProductionTask = CurrentProductionTask;
            productionTaskFile.ProductionTaskID = CurrentProductionTask.ProductionTaskID;
            SaveProductionTaskFileToBD(productionTaskFile);
        }

        private void SaveProductionTaskFileToBD(ProductionTaskFile newFile)
        {
            ProductionTaskFileService productionTaskFileService = new ProductionTaskFileService();
            bool result = productionTaskFileService.AddProductionTaskFile(newFile).Result;
            if (result)
            {
                MessageBox.Show("Добавление файла выполнено успешно");
                ProductionTaskFiles.Add(newFile);
            }
            else
            {
                MessageBox.Show("Ошибка! Добавление файла не выполнено");
            }
        }

        public ICommand IOpenProductionTaskCommentWindow => new RelayCommand(openProductionTaskCommentWindow => OpenProductionTaskCommentWindow());
        private void OpenProductionTaskCommentWindow()
        {
            ProductionTaskCommentWindow productionTaskCommentWindow = new ProductionTaskCommentWindow(CurrentProductionTask, CurrentEmployee);
            productionTaskCommentWindow.ShowDialog();
            ProductionTaskComments = productionTaskCommentWindow.viewModel.ProductionTaskCommentsList;
            ProductionTaskCommentService productionTaskCommentService = new ProductionTaskCommentService();
            bool result = productionTaskCommentService.AddProductionTaskComments(ProductionTaskComments).Result;
            if (!result)
            {
                MessageBox.Show("Ошибка! Комментарии не были сохранены");
            }
        }

        public ICommand IUpdateSubTasks => new RelayCommand(updateSubTasks => UpdateProductionSubTaskStatus());
        private void UpdateProductionSubTaskStatus()
        {
            foreach (ProductionSubTask productionSubTask in ProductionSubTasks)
            {
                UpdateProductionSubTask(productionSubTask);
            }
            GetProductionSubTasks();
        }

        private void UpdateProductionSubTask(ProductionSubTask productionSubTask)
        {
            if (productionSubTask != null)
            {
                ProductionTaskSubService productionTaskSubService = new ProductionTaskSubService();
                bool result = productionTaskSubService.UpdateProductionSubTask(productionSubTask).Result;
                if (!result)
                {
                    MessageBox.Show("Ошибка в обновлении данных");
                }
            }
        }

        public ICommand IOpenExternalDocumentWindow => new RelayCommand(openExternalDocumentWindow => OpenExternalDocumentWindow());
        private void OpenExternalDocumentWindow()
        {
            ExternalDocumentShowWindow externalDocumentShowWindow = new ExternalDocumentShowWindow(ExternalDocument, CurrentEmployee, ExternalDocument.ContractorCompany);
            externalDocumentShowWindow.Show();
        }

        public ICommand IOpenInternalDocumentWindow => new RelayCommand(openInternalDocumentWindow => OpenInternalDocumentWindow());
        private void OpenInternalDocumentWindow()
        {
            InternalDocumentShowWindow internalDocumentShowWindow = new InternalDocumentShowWindow(InternalDocument, CurrentEmployee.EmployeeID);
            internalDocumentShowWindow.Show();
        }

        public ICommand ISendToCreatorEmployee => new RelayCommand(sendToCreatorEmployee => SendToCreatorEmployee());
        private void SendToCreatorEmployee()
        {
            UpdateProductionSubTaskStatus();
            if (CurrentProductionTask.ProductionSubTasks.Count > 0)
            {
                foreach (ProductionSubTask subTask in CurrentProductionTask.ProductionSubTasks)
                {
                    if (subTask.IsDone != true)
                    {
                        MessageBox.Show("Внимание! Одна или несколько подзадач не были выполнены!\n" +
                            "Для отправки на проверку необходимо выполнить все подзадачи.\n" +
                            "В случае невозможности выполнения подзадачи отметьте ее как выполненную\n" +
                            "добавив комментарий или подтверждающие файлы");
                        return;
                    }
                }
            }
            CurrentProductionTask.ProductionTaskStatus = ProductionTaskStatus.UnderInspection;
            ProductionTasksService productionTasksService = new ProductionTasksService();
            bool result = productionTasksService.UpdateProductionTaskStatus(CurrentProductionTask).Result;
            if (result)
            {
                MessageBox.Show($"Статус задачи {CurrentProductionTask.ProductionTaskTitle} был обновлен.\nЗадача направлена на проверку {CurrentProductionTask.EmployeeCreator.EmployeeFullName}");
                Exit();
            }
            else
            {
                MessageBox.Show("Ошибка! Статус задачи не был обновлен");
            }
        }

        public ICommand IConfirmTaskCompletion => new RelayCommand(confirmTaskCompletion => ConfirmTaskCompletion());
        private void ConfirmTaskCompletion()
        {
            UpdateProductionSubTaskStatus();
            MessageBoxResult mainMessageBoxResult = MessageBox.Show("Подтвердить завершение текущей задачи?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mainMessageBoxResult == MessageBoxResult.Yes)
            {
                if (CurrentProductionTask != null && CurrentProductionTask.ProductionSubTasks != null)
                {
                    bool allSubTaskIsDone = true;
                    if (CurrentProductionTask.ProductionSubTasks.Count > 0)
                    {
                        foreach (ProductionSubTask subTask in CurrentProductionTask.ProductionSubTasks)
                        {
                            if (subTask.IsDone != true)
                            {
                                allSubTaskIsDone = false;
                                break;
                            }
                        }
                    }
                    bool TaskIsDone = true;
                    if (!allSubTaskIsDone)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Внимание! Одна или несколько подзадач текущей задачи не выполнены. Подтвердить завершение текущей задачи?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.No)
                        {
                            TaskIsDone = false;
                        }
                    }
                    if (TaskIsDone)
                    {
                        CurrentProductionTask.ProductionTaskStatus = ProductionTaskStatus.Done;
                        ProductionTasksService productionTasksService = new ProductionTasksService();
                        bool result = productionTasksService.UpdateProductionTaskStatus(CurrentProductionTask).Result;
                        if (result)
                        {
                            MessageBox.Show($"Статус задачи {CurrentProductionTask.ProductionTaskTitle} был обновлен.\nВыполнение задачи подтверждено сотрудником: \n{CurrentProductionTask.EmployeeCreator.EmployeeFullName}");
                            Exit();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка! Статус задачи не был обновлен");
                        }
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Направить текущую задачу на доработку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            CurrentProductionTask.ProductionTaskStatus = ProductionTaskStatus.InProgress;
                            ProductionTasksService productionTasksService = new ProductionTasksService();
                            bool result = productionTasksService.UpdateProductionTaskStatus(CurrentProductionTask).Result;
                            if (result)
                            {
                                MessageBox.Show($"Статус задачи {CurrentProductionTask.ProductionTaskTitle} был обновлен.\nЗадача направлена на доработку");
                                Exit();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка! Статус задачи не был обновлен");
                            }
                        }
                    }
                }
            }
            else
            {
                if (CurrentProductionTask != null)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Направить текущую задачу на доработку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        CurrentProductionTask.ProductionTaskStatus = ProductionTaskStatus.InProgress;
                        ProductionTasksService productionTasksService = new ProductionTasksService();
                        bool result = productionTasksService.UpdateProductionTaskStatus(CurrentProductionTask).Result;
                        if (result)
                        {
                            MessageBox.Show($"Статус задачи {CurrentProductionTask.ProductionTaskTitle} был обновлен.\nЗадача направлена на доработку");
                            Exit();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка! Статус задачи не был обновлен");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            ProductionTaskShowWindow.Close();
        }
    }
}

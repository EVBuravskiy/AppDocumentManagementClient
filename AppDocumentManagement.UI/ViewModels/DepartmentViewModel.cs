using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ViewModel for DepartmentWindow
    /// </summary>
    public class DepartmentViewModel : BaseViewModelClass
    {
        /// <summary>
        /// DepartmentWindow announcement
        /// </summary>
        private DepartmentWindow DepartmentWindow;
        /// <summary>
        /// Declaring a variable for the selected department
        /// </summary>
        private Department _selectedDepartment = null;
        /// <summary>
        /// Declaring a variable for the name of the selected department
        /// </summary>
        private string _departmentTitle = "";
        /// <summary>
        /// Declaring a property for the name of the selected department
        /// </summary>
        public string DepartmentTitle
        {
            get => _departmentTitle;
            set
            {
                _departmentTitle = value;
                OnPropertyChanged(nameof(DepartmentTitle));
            }
        }
        /// <summary>
        /// Declaring a variable for the short name of the selected department
        /// </summary>
        private string _departmentShortTitle = "";
        /// <summary>
        /// Declaring a property for the short name of the selected department
        /// </summary>
        public string DepartmentShortTitle
        {
            get => _departmentShortTitle;
            set
            {
                _departmentShortTitle = value;
                OnPropertyChanged(nameof(DepartmentShortTitle));
            }
        }
        /// <summary>
        /// Declaring a variable for the button name to be changed
        /// </summary>
        private string removeBtnTitle = "Очистить данные";
        /// <summary>
        /// Declaring a property for the button name to be changed.
        /// </summary>
        public string RemoveBtnTitle
        {
            get => removeBtnTitle;
            set
            {
                removeBtnTitle = value;
                OnPropertyChanged(nameof(RemoveBtnTitle));
            }
        }
        /// <summary>
        /// DepartmentViewModel constructor
        /// </summary>
        /// <param name="departmentWindow"></param>
        /// <param name="inputDepartment"></param>
        public DepartmentViewModel(DepartmentWindow departmentWindow, Department inputDepartment)
        {
            DepartmentWindow = departmentWindow;
            _selectedDepartment = inputDepartment;
            if (inputDepartment != null)
            {
                DepartmentTitle = _selectedDepartment.DepartmentTitle;
                DepartmentShortTitle = _selectedDepartment.DepartmentShortTitle;
                RemoveBtnTitle = "Удалить отдел";
            }
        }
        /// <summary>
        /// Announcement of the command to delete the selected department
        /// </summary>
        public ICommand IDelete => new RelayCommand(delete => Delete());
        /// <summary>
        /// Function to delete the selected department
        /// </summary>
        private void Delete()
        {
            bool result = false;
            if (_selectedDepartment != null)
            {
                EmployesService employesService = new EmployesService();
                List<Employee> employeesDepartment = employesService.GetEmployeesByDepartmentID(_selectedDepartment.DepartmentID).Result;
                foreach (Employee employee in employeesDepartment)
                {
                    employesService.RemoveEmployee(employee.EmployeeID);
                }
                DepartmentService departmentService = new DepartmentService();
                result = departmentService.RemoveDepartment(_selectedDepartment.DepartmentID).Result;
            }
            if (result)
            {
                MessageBox.Show($"Удаление отдела {DepartmentTitle} выполнено успешно");
                DepartmentTitle = "";
                DepartmentShortTitle = "";
                DepartmentWindow.Close();
                return;
            }
            MessageBox.Show($"Ошибка! Удаление отдела {DepartmentTitle} не выполнено");
            DepartmentWindow.Close();
        }
        /// <summary>
        /// Announcement of the command to save the selected department
        /// </summary>
        public ICommand ISave => new RelayCommand(save => Save());
        /// <summary>
        /// Function to save the selected department
        /// </summary>
        private void Save()
        {
            if (!ValidateDepatment()) return;
            bool result = false;
            if (_selectedDepartment == null)
            {
                Department newDepartment = new Department();
                newDepartment.DepartmentTitle = DepartmentTitle;
                newDepartment.DepartmentShortTitle = DepartmentShortTitle;
                DepartmentService departmentService = new DepartmentService();
                result = departmentService.AddDepartment(newDepartment).Result;
            }
            else
            {
                _selectedDepartment.DepartmentTitle = DepartmentTitle;
                _selectedDepartment.DepartmentShortTitle = DepartmentShortTitle;
                DepartmentService departmentService = new DepartmentService();
                result = departmentService.UpdateDepartment(_selectedDepartment).Result;
            }
            if (result)
            {
                MessageBox.Show($"Сохранение отдела {DepartmentTitle} выполнено успешно");
                DepartmentTitle = "";
                DepartmentShortTitle = "";
                DepartmentWindow.Close();
                return;
            }
            MessageBox.Show($"Ошибка! Сохранение отдела {DepartmentTitle} не выполнено");
        }
        /// <summary>
        /// Function for checking the entered data of the department
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateDepatment()
        {
            if (string.IsNullOrEmpty(DepartmentTitle))
            {
                MessageBox.Show("Введите наименование отдела/департамента");
                DepartmentWindow.DepartmentTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DepartmentWindow.DepartmentTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(DepartmentShortTitle))
            {
                MessageBox.Show("Введите сокращенное наименование отдела/департамента");
                DepartmentWindow.DepartmentShortTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentShortTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DepartmentWindow.DepartmentShortTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentShortTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            return true;
        }
        /// <summary>
        /// Announcement of the command to close DepartmentWindow
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => Exit());
        /// <summary>
        /// Function to close DepartmentWindow
        /// </summary>
        private void Exit()
        {
            DepartmentTitle = "";
            DepartmentShortTitle = "";
            DepartmentWindow.Close();
        }
    }
}

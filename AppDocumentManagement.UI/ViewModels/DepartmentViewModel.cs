using AppDocumentManagement.EmployeeService.Service;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagement.UI.ViewModels
{
    public class DepartmentViewModel : BaseViewModelClass
    {
        private DepartmentWindow DepartmentWindow;

        private Department _selectedDepartment = null;

        private string _departmentTitle = "";
        public string DepartmentTitle
        {
            get => _departmentTitle;
            set
            {
                _departmentTitle = value;
                OnPropertyChanged(nameof(DepartmentTitle));
            }
        }

        private string _departmentShortTitle = "";
        public string DepartmentShortTitle
        {
            get => _departmentShortTitle;
            set
            {
                _departmentShortTitle = value;
                OnPropertyChanged(nameof(DepartmentShortTitle));
            }
        }

        private string removeBtnTitle = "Очистить данные";
        public string RemoveBtnTitle
        {
            get => removeBtnTitle;
            set
            {
                removeBtnTitle = value;
                OnPropertyChanged(nameof(RemoveBtnTitle));
            }
        }
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

        public ICommand IDelete => new RelayCommand(delete => Delete());

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

        public ICommand ISave => new RelayCommand(save => Save());
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

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            DepartmentTitle = "";
            DepartmentShortTitle = "";
            DepartmentWindow.Close();
        }
    }
}

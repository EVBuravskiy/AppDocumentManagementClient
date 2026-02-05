using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ViewModel for DepartmentsEmployeesPanelWindow
    /// </summary>
    public class DepartmentsEmployeesPanelViewModel : BaseViewModelClass
    {
        /// <summary>
        /// DepartmentsEmployeesPanelWindow announcement
        /// </summary>
        private DepartmentsEmployeesPanelWindow DepartmentsEmployeesPanelWindow;
        /// <summary>
        /// Declaration of the current user variable
        /// </summary>
        private Employee currentUser;
        /// <summary>
        /// Declaring a variable that reflects the greeting
        /// </summary>
        private string greeting = string.Empty;
        /// <summary>
        /// Property declaration for greeting
        /// </summary>
        public string Greeting
        {
            get => greeting;
            set
            {
                greeting = value;
                OnPropertyChanged(nameof(Greeting));
            }
        }
        /// <summary>
        /// Declaring a Boolean variable for an employee 
        /// </summary>
        private bool _isEmployee = false;
        /// <summary>
        /// Declaring a variable for a mutable list
        /// </summary>
        private string _labelListContent = "Список отделов";
        /// <summary>
        /// Declaring a property for a mutable list
        /// </summary>
        public string LabelListContent
        {
            get { return _labelListContent; }
            set
            {
                _labelListContent = value;
                OnPropertyChanged(nameof(LabelListContent));
            }
        }
        /// <summary>
        /// Declaring a variable for a changeable button name
        /// </summary>
        private string _addBtnContent = "Добавить отдел";
        /// <summary>
        /// Declaring a property for a changeable button name
        /// </summary>
        public string AddBtnContent
        {
            get { return _addBtnContent; }
            set
            {
                _addBtnContent = value;
                OnPropertyChanged(nameof(AddBtnContent));
            }
        }
        /// <summary>
        /// Declaring a property for a list of all departments
        /// </summary>
        private List<Department> allDepartments { get; set; }
        /// <summary>
        /// Declaring a property for a observable collection of all departments
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }
        /// <summary>
        /// Declaring a variable for the selected department
        /// </summary>
        private Department _selectedDepartment = null;
        /// <summary>
        /// Declaring a property for the selected department
        /// </summary>
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
                if (value != null)
                {
                    GetEmployeesOfDepartment();
                    GetDeptyOfDepartment();
                    GetHeadOfDepartment();
                    GetPerformersOfDepartment();
                }
            }
        }
        /// <summary>
        /// Declaring a property for a list of employee photos
        /// </summary>
        private List<EmployeePhoto> EmployeePhotos { get; set; }
        /// <summary>
        /// Declaring a property for a list of employees
        /// </summary>
        private List<Employee> allEmployees { get; set; }
        /// <summary>
        /// Declaring a property for employees in an ObservableCollection
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        /// <summary>
        /// Declaring a variable for the selected employee
        /// </summary>
        private Employee _selectedEmployee = null;
        /// <summary>
        /// Declaring a property for the selected employee
        /// </summary>
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        /// <summary>
        /// Declaring a property for employees of the selected department in an ObservableCollection
        /// </summary>
        public ObservableCollection<Employee> EmployeesOfDepartment { get; set; }
        /// <summary>
        /// Declaring a variable for the general director
        /// </summary>
        private Employee _deputyGeneralDirector;
        /// <summary>
        /// Declaring a property for the general director
        /// </summary>
        public Employee DeputyGeneralDirector
        {
            get => _deputyGeneralDirector;
            set
            {
                _deputyGeneralDirector = value;
                OnPropertyChanged(nameof(DeputyGeneralDirector));
            }
        }
        /// <summary>
        /// Declaring a variable for the head of selected department
        /// </summary>
        private Employee _headOfDepartment;
        /// <summary>
        /// Declaring a property for the head of selected department
        /// </summary>
        public Employee HeadOfDepartment
        {
            get => _headOfDepartment;
            set
            {
                _headOfDepartment = value;
                OnPropertyChanged(nameof(HeadOfDepartment));
            }
        }
        /// <summary>
        /// Declaring a property for the performers of the selected department in an ObservableCollection
        /// </summary>
        public ObservableCollection<Employee> PerformersOfDepartment { get; set; }
        /// <summary>
        /// Declaring a variable for the selected performer
        /// </summary>
        private Employee _selectedPerformer = null;
        /// <summary>
        /// Declaring a property for the selected performer
        /// </summary>
        public Employee SelectedPerformer
        {
            get => _selectedPerformer;
            set
            {
                _selectedPerformer = value;
                OnPropertyChanged(nameof(SelectedPerformer));
                if (value != null)
                {
                    UpdatePerformer();
                }
            }
        }
        /// <summary>
        /// Declaring a variable for a search string
        /// </summary>
        private string _searchString;
        /// <summary>
        /// Declaring a property for a search string
        /// </summary>
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }
        /// <summary>
        /// Declaring a variable for the department size
        /// </summary>
        private int departmentSize;
        /// <summary>
        /// Declaring a property for the department size
        /// </summary>
        public int DepartmentSize
        {
            get => departmentSize;
            set
            {
                departmentSize = value;
                OnPropertyChanged(nameof(DepartmentSize));
            }
        }
        /// <summary>
        /// DepartmentsEmployeesPanelViewModel constructor
        /// </summary>
        /// <param name="inputWindow"></param>
        /// <param name="currentUserID"></param>
        public DepartmentsEmployeesPanelViewModel(DepartmentsEmployeesPanelWindow inputWindow, int currentUserID)
        {
            DepartmentsEmployeesPanelWindow = inputWindow;
            InitializeCurrentUser(currentUserID);
            Departments = new ObservableCollection<Department>();
            EmployeesOfDepartment = new ObservableCollection<Employee>();
            PerformersOfDepartment = new ObservableCollection<Employee>();
            GetEmployeesPhotos();
            InitializeDepartments();
            InitializeEmployees();
            SelectedDepartment = Departments.FirstOrDefault();
        }
        /// <summary>
        /// Function to initialize the current user
        /// </summary>
        /// <param name="currentUserID"></param>
        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployesService employesService = new EmployesService();
            currentUser = employesService.GetEmployeeByID(currentUserID).Result;
            Greeting = $"Добрый день, {currentUser.EmployeeFirstMiddleName}!";
        }
        /// <summary>
        /// Function to get and initialize a list of departments
        /// </summary>
        private void InitializeDepartments()
        {
            Departments.Clear();
            DepartmentService departmentService = new DepartmentService();
            allDepartments = departmentService.GetAllDepartments().Result;
            if (allDepartments.Count > 0)
            {
                foreach (Department department in allDepartments)
                {
                    Departments.Add(department);
                }
            }
        }
        /// <summary>
        /// Function for getting a list of employee photos
        /// </summary>
        private void GetEmployeesPhotos()
        {
            EmployeePhotos = new List<EmployeePhoto>();
            EmployeePhotoService employeePhotoService = new EmployeePhotoService();
            try
            {
                EmployeePhotos = employeePhotoService.GetEmployeePhotos().Result;
            }
            catch
            {
                Console.WriteLine("Ошибка загрузки фотографий");
            }
            if (EmployeePhotos.Count > 0)
            {
                foreach (EmployeePhoto photo in EmployeePhotos)
                {
                    string photoPath = FileProcessing.SaveEmployeePhotoToTempFolder(photo);
                    photo.FilePath = photoPath;
                }
            }
        }
        /// <summary>
        /// Function to get and initialize an observable collection of employees
        /// </summary>
        private void InitializeEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            EmployesService employesService = new EmployesService();
            allEmployees = employesService.GetAllEmployees().Result;
            if (allEmployees.Count > 0)
            {
                foreach (Employee employee in allEmployees)
                {
                    Department department = Departments.SingleOrDefault(x => x.DepartmentID == employee.DepartmentID);
                    if(department != null)
                    {
                        employee.Department = department;
                        employee.DepartmentID = department.DepartmentID;
                    }
                    EmployeePhoto employeePhoto = EmployeePhotos.SingleOrDefault(p => p.EmployeeID == employee.EmployeeID);
                    if (employeePhoto != null)
                    {
                        employee.EmployeePhoto = employeePhoto;
                        employee.EmployeePhotoID = employeePhoto.EmployeePhotoID;
                    }
                }
            }
            if (allEmployees.Count > 0)
            {
                foreach (Employee employee in allEmployees)
                {
                    Employees.Add(employee);
                }
            }
            OnPropertyChanged(nameof(Employees));
        }
        /// <summary>
        /// Function for obtaining a list of employees of the selected department
        /// </summary>
        private void GetEmployeesOfDepartment()
        {
            EmployeesOfDepartment.Clear();
            if (SelectedDepartment != null) {
                foreach (Employee employee in Employees)
                {
                    if (employee.DepartmentID == SelectedDepartment.DepartmentID)
                    {
                        EmployeesOfDepartment.Add(employee);
                    }
                }
            }
            DepartmentSize = EmployeesOfDepartment.Count;
            OnPropertyChanged(nameof(EmployeesOfDepartment));
        }
        /// <summary>
        /// Function for obtaining an employee holding the position of Deputy General Director of the selected department
        /// </summary>
        private void GetDeptyOfDepartment()
        {
            DeputyGeneralDirector = new Employee();
            if(SelectedDepartment != null && EmployeesOfDepartment.Count > 0)
            {
                DeputyGeneralDirector = EmployeesOfDepartment.Where(x => x.EmployeeRole == EmployeeRole.DeputyGeneralDirector).FirstOrDefault();
            }
        }
        /// <summary>
        /// Function for obtaining an employee holding the position of head of the selected department
        /// </summary>
        private void GetHeadOfDepartment()
        {
            HeadOfDepartment = new Employee();
            if (SelectedDepartment != null && EmployeesOfDepartment.Count > 0)
            {
                HeadOfDepartment = EmployeesOfDepartment.Where(x => x.EmployeeRole == EmployeeRole.HeadOfDepartment).FirstOrDefault();
            }
        }
        /// <summary>
        /// Function for obtaining a list of performers of the selected department
        /// </summary>
        private void GetPerformersOfDepartment()
        {
            PerformersOfDepartment.Clear();
            if (SelectedDepartment != null && EmployeesOfDepartment.Count > 0)
            {
                foreach (Employee employee in EmployeesOfDepartment)
                {
                    if(employee.EmployeeRole == EmployeeRole.Performer)
                    {
                        PerformersOfDepartment.Add(employee);
                    }
                }
            }
            OnPropertyChanged(nameof(PerformersOfDepartment));
        }
        /// <summary>
        /// Declaring a command to show departments
        /// </summary>
        public ICommand ISelectDepartments => new RelayCommand(selectDepartments => SelectDepartments());
        /// <summary>
        /// Function to show departments
        /// </summary>
        private void SelectDepartments()
        {
            LabelListContent = "Список отделов";
            AddBtnContent = "Добавить отдел";
            DepartmentsEmployeesPanelWindow.DepartmentList.Visibility = Visibility.Visible;
            DepartmentsEmployeesPanelWindow.EmployeeList.Visibility = Visibility.Hidden;
            DepartmentsEmployeesPanelWindow.DetailDepartmentInfo.Visibility = Visibility.Visible;
            DepartmentsEmployeesPanelWindow.EmployeeDetailInfo.Visibility = Visibility.Hidden;
            _isEmployee = false;
            InitializeDepartments();
            InitializeEmployees();
            SelectedDepartment = Departments.FirstOrDefault();
        }
        /// <summary>
        /// Declaring a command to show employees
        /// </summary>
        public ICommand ISelectEmployees => new RelayCommand(selectEmployees => SelectEmployees());
        /// <summary>
        /// Function to show employees
        /// </summary>
        private void SelectEmployees()
        {
            LabelListContent = "Список сотрудников";
            AddBtnContent = "Добавить сотрудника";
            DepartmentsEmployeesPanelWindow.DepartmentList.Visibility = Visibility.Hidden;
            DepartmentsEmployeesPanelWindow.EmployeeList.Visibility = Visibility.Visible;
            DepartmentsEmployeesPanelWindow.DetailDepartmentInfo.Visibility = Visibility.Hidden;
            DepartmentsEmployeesPanelWindow.EmployeeDetailInfo.Visibility = Visibility.Visible;
            _isEmployee = true;
            SelectedEmployee = Employees.FirstOrDefault();
        }
        /// <summary>
        /// Declaring a search command
        /// </summary>
        public ICommand IFindItem => new RelayCommand(findItem => FindItem());
        /// <summary>
        /// Search function 
        /// </summary>
        private void FindItem()
        {
            FindItems(SearchString);
        }
        /// <summary>
        /// Search function by search bar
        /// </summary>
        /// <param name="searchString"></param>
        public void FindItems(string searchString)
        {
            SearchString = searchString;
            if (!_isEmployee)
            {
                FindDepartment();
            }
            else
            {
                FindEmployee();
            }
        }
        /// <summary>
        /// Department search function
        /// </summary>
        private void FindDepartment()
        {
            Departments.Clear();
            if(string.IsNullOrEmpty(SearchString) || string.IsNullOrWhiteSpace(SearchString)) 
            {
                if (allDepartments.Count > 0)
                {
                    foreach (Department department in allDepartments)
                    {
                        Departments.Add(department);
                    }
                }
                SelectedDepartment = Departments.FirstOrDefault();
                return;
            }
            List<Department> findingDepartment = new List<Department>();
            string tempSearchString = SearchString.ToLower().Trim();
            findingDepartment = allDepartments.Where(x => x.DepartmentTitle.ToLower().Contains(tempSearchString)).ToList();
            if (findingDepartment.Count == 0)
            {
                findingDepartment = allDepartments.Where(x => x.DepartmentShortTitle.ToLower().Contains(tempSearchString)).ToList();
            }
            foreach (Department department in findingDepartment)
            {
                Departments.Add(department);
            }
            SelectedDepartment = Departments.FirstOrDefault();
        }
        /// <summary>
        /// Employee search function
        /// </summary>
        private void FindEmployee()
        {
            Employees.Clear();
            if (string.IsNullOrEmpty(SearchString) || string.IsNullOrWhiteSpace(SearchString))
            {
                if (allEmployees.Count > 0)
                {
                    foreach (Employee employee in allEmployees)
                    {
                        Employees.Add(employee);
                    }
                }
                SelectedEmployee = Employees.FirstOrDefault();
                return;
            }
            List<Employee> findingEmployee = new List<Employee>();
            string tempSearchString = SearchString.ToLower().Trim();
            findingEmployee = allEmployees.Where(x => x.EmployeeFullName.ToLower().Contains(tempSearchString)).ToList();
            if (findingEmployee.Count > 0)
            {
                foreach (Employee employee in findingEmployee)
                {
                    Employees.Add(employee);
                }
            }
            SelectedEmployee = Employees.FirstOrDefault();
        }
        /// <summary>
        /// Declaring a command to add a new element
        /// </summary>
        public ICommand IAddItem => new RelayCommand(addItem => AddItem());
        /// <summary>
        /// Function to add a new element
        /// </summary>
        private void AddItem()
        {
            if (!_isEmployee)
            {
                AddNewDepartment();
            }
            else
            {
                AddNewEmployee();
            }
        }
        /// <summary>
        /// Declaring a command to add a new department
        /// </summary>
        public ICommand IAddNewDepartment => new RelayCommand(addNewDepartment => AddNewDepartment());
        /// <summary>
        /// Function to add a new department
        /// </summary>
        private void AddNewDepartment()
        {
            DepartmentWindow departmentWindow = new DepartmentWindow(null);
            departmentWindow.ShowDialog();
            Departments.Clear();
            InitializeDepartments();
            if (SelectedDepartment != null)
            {
                Department department = Departments.Where(x => x.DepartmentID == SelectedDepartment.DepartmentID).FirstOrDefault();
                if (department != null)
                {
                    SelectedDepartment = department;
                }
            }
            else
            {
                SelectedDepartment = Departments.FirstOrDefault();
            }
        }
        /// <summary>
        /// Declare a command to edit the selected department
        /// </summary>
        public ICommand IEditSelectedDepartment => new RelayCommand(editSelectedDepartment => EditSelectedDepartment());
        /// <summary>
        /// Function to edit the selected department
        /// </summary>
        private void EditSelectedDepartment()
        {
            if (SelectedDepartment != null)
            {
                DepartmentWindow departmentWindow = new DepartmentWindow(SelectedDepartment);
                departmentWindow.ShowDialog();
                Departments.Clear();
                InitializeDepartments();
                InitializeEmployees();
                OnPropertyChanged(nameof(Employees));
                if (SelectedDepartment != null)
                {
                    Department department = Departments.Where(x => x.DepartmentID == SelectedDepartment.DepartmentID).FirstOrDefault();
                    if (department != null)
                    {
                        SelectedDepartment = department;
                    }
                }
                else
                {
                    SelectedDepartment = Departments.FirstOrDefault();
                }
            }
        }
        /// <summary>
        /// Declaring a command to add a new employee
        /// </summary>
        public ICommand IAddNewEmployee => new RelayCommand(addEmployee => AddNewEmployee());
        /// <summary>
        /// Function to add a new employee
        /// </summary>
        private void AddNewEmployee()
        {
            EmployeeWindow employeeWindow = new EmployeeWindow(null);
            employeeWindow.ShowDialog();
            Employees.Clear();
            GetEmployeesPhotos();
            InitializeEmployees();
            if (SelectedEmployee != null)
            {
                Employee employee = Employees.Where(x => x.EmployeeID == SelectedEmployee.EmployeeID).FirstOrDefault();
                if (employee != null)
                {
                    SelectedEmployee = employee;
                }
            }
            else
            {
                SelectedEmployee = Employees.FirstOrDefault();
            }
            if(SelectedDepartment != null)
            {
                GetEmployeesOfDepartment();
                GetDeptyOfDepartment();
                GetHeadOfDepartment();
                GetPerformersOfDepartment();
            }
        }
        /// <summary>
        /// Announce a command to update the selected employee's data.
        /// </summary>
        public ICommand IUpdateEmployee => new RelayCommand(updateEmployee => UpdateEmployee());
        /// <summary>
        /// Function to update the selected employee's data
        /// </summary>
        private void UpdateEmployee()
        {
            if (SelectedEmployee != null)
            {
                UpdateCurrentEmployee(SelectedEmployee);
            }
        }
        /// <summary>
        /// Function for updating data of the selected performer
        /// </summary>
        private void UpdatePerformer()
        {
            if (SelectedPerformer != null)
            {
                UpdateCurrentEmployee(SelectedPerformer);
                if (SelectedDepartment != null)
                {
                    GetEmployeesOfDepartment();
                    GetPerformersOfDepartment();
                }
            }
            SelectedPerformer = null;
        }
        /// <summary>
        /// Function for updating data of the current employee.
        /// </summary>
        /// <param name="inputEmployee"></param>
        private void UpdateCurrentEmployee(Employee inputEmployee)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow(inputEmployee);
            employeeWindow.ShowDialog();
            Employees.Clear();
            GetEmployeesPhotos();
            InitializeEmployees();
            if (SelectedEmployee != null)
            {
                Employee employee = Employees.Where(x => x.EmployeeID == SelectedEmployee.EmployeeID).FirstOrDefault();
                if (employee != null)
                {
                    SelectedEmployee = employee;
                }
            }
            else
            {
                SelectedEmployee = Employees.FirstOrDefault();
            }
        }
        /// <summary>
        /// Announcement of a command to remove a selected employee
        /// </summary>
        public ICommand IRemoveEmployee => new RelayCommand(removeEmployee => RemoveEmployee());
        /// <summary>
        /// Function to remove a selected employee
        /// </summary>
        private void RemoveEmployee()
        {
            if (SelectedEmployee != null)
            {
                EmployesService employesService = new EmployesService();
                bool result = employesService.RemoveEmployee(SelectedEmployee.EmployeeID).Result;
                if (result)
                {
                    MessageBox.Show($"Удаление {SelectedEmployee.EmployeeFullName} выполнено");
                }
                else
                {
                    MessageBox.Show($"Ошибка. Удаление {SelectedEmployee.EmployeeFullName} не выполнено");
                }
            }
            InitializeEmployees();
        }
        /// <summary>
        /// Declaring the command to open the manager panel window
        /// </summary>
        public ICommand IOpenManagerPanelWindow => new RelayCommand(openManagerPanelWindow => OpenManagerPanelWindow());
        /// <summary>
        /// Function to open the manager panel window
        /// </summary>
        private void OpenManagerPanelWindow()
        {
            ManagerPanelWindow managerPanelWindow = new ManagerPanelWindow(currentUser.EmployeeID);
            managerPanelWindow.Show();
        }
        /// <summary>
        /// Declaring the command to close DepartmentsEmployeesPanelWindow
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => Exit());
        /// <summary>
        /// Function to close DepartmentsEmployeesPanelWindow
        /// </summary>
        private void Exit()
        {
            DepartmentsEmployeesPanelWindow.Close();
        }
    }
}

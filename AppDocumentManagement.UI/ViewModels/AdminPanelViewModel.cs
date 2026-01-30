using AppDocumentManagement.UI.Views;
using System.Windows.Input;
using AppDocumentManagement.Models;
using AppDocumentManagement.EmployeesService.Service;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ModelView for AdminPanelWindow view
    /// </summary>
    public class AdminPanelViewModel : BaseViewModelClass
    {
        /// <summary>
        /// AdminPanelWindow announcement
        /// </summary>
        private AdminPanelWindow _adminPanelWindow;
        /// <summary>
        /// Employee announcement
        /// </summary>
        private Employee currentUser;
        /// <summary>
        /// Greeting string announcement
        /// </summary>
        private string greating;
        /// <summary>
        /// Greeting string property
        /// </summary>
        public string Greating
        {
            get => greating;
            set
            {
                greating = value;
                OnPropertyChanged(nameof(Greating));
            }
        }
        /// <summary>
        /// AdminPanelViewModel constructor
        /// </summary>
        /// <param name="adminPanelWindow"></param>
        /// <param name="currentUserID"></param>
        public AdminPanelViewModel(AdminPanelWindow adminPanelWindow, int currentUserID)
        {
            _adminPanelWindow = adminPanelWindow;
            InitializeCurrentUser(currentUserID);
        }
        /// <summary>
        /// Current user initialization function
        /// </summary>
        /// <param name="currentUserID"></param>
        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployesService employesService = new EmployesService();
            currentUser = employesService.GetEmployeeByID(currentUserID).Result;
            Greating = $"Добрый день, {currentUser.EmployeeFirstMiddleName}!";
        }
        /// <summary>
        /// Announcing the DepartmentsEmployeesPanelWindow opening command
        /// </summary>
        public ICommand IOpenPersonellRecordsWindow => new RelayCommand(openDepartment => OpenPersonnelRecordsWindow());
        /// <summary>
        /// Opening the DepartmentsEmployeesPanelWindow function
        /// </summary>
        private void OpenPersonnelRecordsWindow()
        {
            DepartmentsEmployeesPanelWindow departmentsEmployeesPanelWindow = new DepartmentsEmployeesPanelWindow(currentUser.EmployeeID);
            departmentsEmployeesPanelWindow.Show();
            //_adminPanelWindow.Close();
        }
        /// <summary>
        /// Announcing the UserRegistrationWindow opening command
        /// </summary>
        public ICommand IOpenUserRegistrationWindow => new RelayCommand(openUserRegistrationWindow => OpenUserRegistrationWindow());
        /// <summary>
        /// Opening the UserRegistrationWindow function
        /// </summary>
        private void OpenUserRegistrationWindow()
        {
            UserRegistrationWindow userRegistrationWindow = new UserRegistrationWindow(currentUser.EmployeeID);
            userRegistrationWindow.Show();
            _adminPanelWindow.Close();
        }
        /// <summary>
        /// Announcing the DocumentRegistrationWindow opening command
        /// </summary>
        public ICommand IOpenDocumentRegistrationWindow => new RelayCommand(openDocumentRegistrationWindow => OpenDocumentRegistrationWindow());
        /// <summary>
        /// Opening the DocumentRegistrationWindow function
        /// </summary>
        private void OpenDocumentRegistrationWindow()
        {
            DocumentRegistrationWindow documentRegistrationWindow = new DocumentRegistrationWindow(currentUser.EmployeeID);
            documentRegistrationWindow.Show();
        }
        /// <summary>
        /// Announcing the ManagerPanelWindow opening command
        /// </summary>
        public ICommand IOpenManagerPanelWindow => new RelayCommand(openManagerPanelWindow => OpenManagerPanelWindow());
        /// <summary>
        /// Opening the ManagerPanelWindow function
        /// </summary>
        private void OpenManagerPanelWindow()
        {
            ManagerPanelWindow managerPanelWindow = new ManagerPanelWindow(currentUser.EmployeeID);
            managerPanelWindow.Show();
        }
        /// <summary>
        /// ManagerPanelWindow close command
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => { _adminPanelWindow.Close(); });
    }
}

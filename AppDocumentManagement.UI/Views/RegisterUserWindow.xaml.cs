using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserWindow.xaml
    /// </summary>
    public partial class RegisterUserWindow : Window
    {
        private RegisterUserViewModel _viewModel;

        public RegisterUserWindow(Employee selectedEmployee, bool isRegistred)
        {
            InitializeComponent();
            _viewModel = new RegisterUserViewModel(this, selectedEmployee, isRegistred);
            DataContext = _viewModel;
        }

        private void EmployeeLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowLoginRequirements();
        }

        private void EmployeePassword_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowPasswordRequirements();
        }

        private void EmployeeLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowDefaultRequirements();
        }

        private void EmployeePassword_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowDefaultRequirements();
        }
    }
}

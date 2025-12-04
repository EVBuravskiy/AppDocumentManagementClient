using AppDocumentManagement.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для UserRegistrationWindow.xaml
    /// </summary>
    public partial class UserRegistrationWindow : Window
    {
        private UserRegistrationViewModel _viewModel;
        public UserRegistrationWindow(int currentUserID)
        {
            _viewModel = new UserRegistrationViewModel(this, currentUserID);
            InitializeComponent();
            DataContext = _viewModel;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _viewModel.SearchString = textBox.Text;
            _viewModel.FindUserBySearchString(textBox.Text);
        }
    }
}

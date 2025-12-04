using AppDocumentManagement.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для InitialAuthorizationWindow.xaml
    /// </summary>
    public partial class InitialAuthorizationWindow : Window
    {
        private InitialAuthorizationViewModel viewModel;
        private bool visible = false;
        public InitialAuthorizationWindow()
        {
            viewModel = new InitialAuthorizationViewModel(this);
            InitializeComponent();
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextPasswordBox.Text = PasswordBox.Password;
            viewModel.Password = TextPasswordBox.Text;
        }

        private void ShowPass(object sender, RoutedEventArgs e)
        {
            if (visible == false)
            {
                PasswordBox.Visibility = Visibility.Hidden;
                TextPasswordBox.Visibility = Visibility.Visible;
                visible = true;
            }
            else
            {
                PasswordBox.Password = TextPasswordBox.Text;
                TextPasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
                visible = false;
            }
        }
    }
}

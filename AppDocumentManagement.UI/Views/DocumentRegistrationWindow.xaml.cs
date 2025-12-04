using AppDocumentManagement.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для DocumentRegistrationWindow.xaml
    /// </summary>
    public partial class DocumentRegistrationWindow : Window
    {
        private DocumentRegistrationViewModel viewModel;
        public DocumentRegistrationWindow(int currentUserID)
        {
            viewModel = new DocumentRegistrationViewModel(this, currentUserID);
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            viewModel.SearchString = textBox.Text;
            viewModel.GetDocumentBySearchString(textBox.Text);
        }
    }
}

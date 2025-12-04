using AppDocumentManagement.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ContractorСompanyListWindow.xaml
    /// </summary>
    public partial class ContractorСompanyListWindow : Window
    {
        public ContractorCompanyListViewModel viewModel;
        public ContractorСompanyListWindow()
        {
            InitializeComponent();
            viewModel = new ContractorCompanyListViewModel(this);
            DataContext = viewModel;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            viewModel.SearchString = textBox.Text;
            viewModel.GetContractorCompanyBySearchString(textBox.Text);
        }
    }
}

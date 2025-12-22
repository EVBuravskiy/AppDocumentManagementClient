using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ExternalDocumentShowWindow.xaml
    /// </summary>
    public partial class ExternalDocumentShowWindow : Window
    {
        private ExternalDocumentShowViewModel viewModel;
        public ExternalDocumentShowWindow(ExternalDocument inputExternalDocument, Employee currentEmployee, ContractorCompany documentContractorCompany)
        {
            InitializeComponent();
            viewModel = new ExternalDocumentShowViewModel(this, inputExternalDocument, documentContractorCompany, currentEmployee);
            DataContext = viewModel;
        }
    }
}

using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ContractorCompanyWindow.xaml
    /// </summary>
    public partial class ContractorCompanyWindow : Window
    {
        private ContractorCompanyViewModel contractorCompanyViewModel;
        public ContractorCompanyWindow(ContractorCompany company)
        {
            contractorCompanyViewModel = new ContractorCompanyViewModel(this, company);
            InitializeComponent();
            DataContext = contractorCompanyViewModel;
        }
    }
}

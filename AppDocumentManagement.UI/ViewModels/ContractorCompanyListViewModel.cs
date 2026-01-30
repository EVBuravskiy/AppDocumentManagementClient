using AppDocumentManagement.ExternalDocumentService.Services;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ViewModel for ContractorСompanyListWindow
    /// </summary>
    public class ContractorCompanyListViewModel : BaseViewModelClass
    {
        /// <summary>
        /// ContractorСompanyListWindow announcement
        /// </summary>
        ContractorСompanyListWindow Window;
        /// <summary>
        /// Declaring a property for the ContractorCompany list
        /// </summary>
        private List<ContractorCompany> ContractorCompaniesList {  get; set; }
        /// <summary>
        /// Declaring a property for the ContractorCompany observable collection
        /// </summary>
        public ObservableCollection<ContractorCompany> ContractorCompanies {  get; set; }
        /// <summary>
        /// Declaring ContractorCompany for selection
        /// </summary>
        private ContractorCompany selectedContractorCompany;
        /// <summary>
        /// Declaring a property for selection ContractorCompany
        /// </summary>
        public ContractorCompany SelectedContractorCompany
        {
            get => selectedContractorCompany;
            set
            {
                selectedContractorCompany = value;
                OnPropertyChanged(nameof(SelectedContractorCompany));
            }
        }
        /// <summary>
        /// Declaring search string
        /// </summary>
        private string _searchString;
        /// <summary>
        /// Declaring a property for search string
        /// </summary>
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                GetContractorCompanyBySearchString(value);
            }
        }
        /// <summary>
        /// ContractorCompanyListViewModel constructor
        /// </summary>
        /// <param name="window"></param>
        public ContractorCompanyListViewModel(ContractorСompanyListWindow window) 
        {
            Window = window;
            ContractorCompanies = new ObservableCollection<ContractorCompany>();
            GetListOfContractorCompanies();
            InitializeContractorCompanies();
        }
        /// <summary>
        /// Function to get contractor companies list
        /// </summary>
        private void GetListOfContractorCompanies()
        {
            if(ContractorCompaniesList == null)
            {
                ContractorCompaniesList = new List<ContractorCompany>();
            }
            else
            {
                ContractorCompaniesList.Clear();
            }
            ContractorCompanyService contractorCompanyService = new ContractorCompanyService();
            ContractorCompaniesList = contractorCompanyService.GetContractorCompanies().Result;
        }
        /// <summary>
        /// Function to initialize contractor companies observable collection
        /// </summary>
        private void InitializeContractorCompanies()
        {
            if (ContractorCompaniesList == null)
            {
                ContractorCompanies = new ObservableCollection<ContractorCompany>();
            }
            else
            {
                ContractorCompanies.Clear();
            }
            if (ContractorCompaniesList?.Count > 0)
            {
                foreach (ContractorCompany company in ContractorCompaniesList)
                {
                    ContractorCompanies.Add(company);
                }
            }
        }
        /// <summary>
        /// Function for searching for a contractor company using a search string
        /// </summary>
        /// <param name="searchingString"></param>
        public void GetContractorCompanyBySearchString(string searchingString)
        {
            string searchString = searchingString.Trim();
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                InitializeContractorCompanies();
                return;
            }   
            ContractorCompanies.Clear();
            if (ContractorCompaniesList?.Count > 0)
            {
                foreach (var company in ContractorCompaniesList)
                {
                    if (company.ContractorCompanyTitle.ToLower().Contains(searchString.ToLower()))
                    {
                        ContractorCompanies.Add(company);
                    }
                }
            }
        }
        /// <summary>
        /// Declaring command to add a new contractor company 
        /// </summary>
        public ICommand IAddNewContractorCompany => new RelayCommand(addNewContractorCompany => AddNewContractorCompany());
        /// <summary>
        /// Function to add a new contractor company
        /// </summary>
        private void AddNewContractorCompany()
        {
            ContractorCompanyWindow contractorCompanyWindow = new ContractorCompanyWindow(null);
            contractorCompanyWindow.ShowDialog();
            GetListOfContractorCompanies();
            InitializeContractorCompanies();
        }
        /// <summary>
        /// Declaring command to edit the selected contractor company 
        /// </summary>
        public ICommand IEditContractorCompany => new RelayCommand(editContractorCompany => EditContractorCompany());
        /// <summary>
        /// Function for editing selected contractor company
        /// </summary>
        private void EditContractorCompany()
        {
            if (SelectedContractorCompany != null)
            {
                ContractorCompanyWindow contractorCompanyWindow = new ContractorCompanyWindow(SelectedContractorCompany);
                contractorCompanyWindow.ShowDialog();
                GetListOfContractorCompanies();
                InitializeContractorCompanies();
            }
        }
        /// <summary>
        /// Declaring command to select contractor company 
        /// </summary>
        public ICommand ISelectContractorCompany => new RelayCommand(selectContractorCompany => SelectContractorCompany());
        /// <summary>
        /// Function to close ContractorСompanyListWindow 
        /// </summary>
        private void SelectContractorCompany()
        {
            Window.Close();
        }
        /// <summary>
        /// Function to close ContractorСompanyListWindow 
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => Window.Close());
    }
}

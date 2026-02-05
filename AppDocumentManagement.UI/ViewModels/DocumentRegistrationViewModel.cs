using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.ExternalDocumentService.Services;
using AppDocumentManagement.InternalDocumentService.Services;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ViewModel for DocumentRegistrationWindow
    /// </summary>
    public class DocumentRegistrationViewModel : BaseViewModelClass
    {
        /// <summary>
        /// DocumentRegistrationWindow announcement
        /// </summary>
        private DocumentRegistrationWindow DocumentRegistrationWindow;
        /// <summary>
        /// Declaration of the current user variable
        /// </summary>
        private Employee currentUser;
        /// <summary>
        /// Declaring a variable that reflects the greeting
        /// </summary>
        private string greating;
        /// <summary>
        /// Property declaration for greeting
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
        /// Declaring a variable for a list of employees
        /// </summary>
        private List<Employee> Employees;
        /// <summary>
        /// Declaring a variable for a list of contractor companies
        /// </summary>
        private List<ContractorCompany> ContractorCompanies;
        /// <summary>
        /// Declaring a variable for a list of departments
        /// </summary>
        private List<Department> Departments;
        /// <summary>
        /// Declaring a variable for a list of external documents
        /// </summary>
        private List<ExternalDocument> ExternalDocumentList;
        /// <summary>
        /// Declaring a variable for an observable collection of employees
        /// </summary>
        public ObservableCollection<ExternalDocument> ExternalDocumentsCollection { get; set; }
        /// <summary>
        /// Declaring a variable for the selected external document
        /// </summary>
        private ExternalDocument selectedExternalDocument;
        /// <summary>
        /// Declaring a property for the selected external document
        /// </summary>
        public ExternalDocument SelectedExternalDocument
        {
            get => selectedExternalDocument;
            set
            {
                selectedExternalDocument = value;
                OnPropertyChanged(nameof(SelectedExternalDocument));
                if (SelectedExternalDocument != null)
                {
                    OpenDocumentWindow(SelectedExternalDocument);
                }
            }
        }
        /// <summary>
        /// Declaring a property for the observablecollection of external document types
        /// </summary>
        public ObservableCollection<string> ExternalDocumentTypes { get; private set; }

        private int selectedExternalDocumentTypeIndex;
        public int SelectedExternalDocumentTypeIndex
        {
            get => selectedExternalDocumentTypeIndex;
            set
            {
                selectedExternalDocumentTypeIndex = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentTypeIndex));
            }
        }
        /// <summary>
        /// Declaring a variable for the selected external document type
        /// </summary>
        private string selectedExternalDocumentType;
        /// <summary>
        /// Declaring a property for the selected external document type
        /// </summary>
        public string SelectedExternalDocumentType
        {
            get => selectedExternalDocumentType;
            set
            {
                selectedExternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentType));
                int index = 0;
                for (; index < ExternalDocumentTypes.Count; index++)
                {
                    if (value.Equals(ExternalDocumentTypes[index]))
                    {
                        SelectedExternalDocumentTypeIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                    else
                    {
                        GetDocumentsByDocumentType();
                    }
                }
            }
        }
        /// <summary>
        /// Declaring a variable for a list of internal documents
        /// </summary>
        private List<InternalDocument> InternalDocumentList;
        /// <summary>
        /// Declaring a property for an observablecollection of internal documents
        /// </summary>
        public ObservableCollection<InternalDocument> InternalDocumentsCollection { get; set; }
        /// <summary>
        /// Declaring a variable for the selected internal document
        /// </summary>
        private InternalDocument selectedInternalDocument;
        /// <summary>
        /// Declaring a property for the selected internal document
        /// </summary>
        public InternalDocument SelectedInternalDocument
        {
            get => selectedInternalDocument;
            set
            {
                selectedInternalDocument = value;
                OnPropertyChanged(nameof(SelectedInternalDocument));
                if (SelectedInternalDocument != null)
                {
                    OpenInternalDocumentWindow(SelectedInternalDocument);
                }
            }
        }
        /// <summary>
        /// Declaring a property for an observablecollection of internal document statuses
        /// </summary>
        public ObservableCollection<string> InternalDocumentRegistationStatus { get; set; }
        /// <summary>
        /// Declaring a variable for an index of internal document status
        /// </summary>
        private int selectedInternalDocumentRegistationStatusIndex;
        /// <summary>
        /// Declaring a property for an index of internal document status
        /// </summary>
        public int SelectedInternalDocumentRegistationStatusIndex
        {
            get => selectedInternalDocumentRegistationStatusIndex;
            set
            {
                selectedInternalDocumentRegistationStatusIndex = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentRegistationStatusIndex));
            }
        }
        /// <summary>
        /// Declaring a variable for the selected index of internal document status
        /// </summary>
        private string selectedInternalDocumentRegistationStatus;
        /// <summary>
        /// Declaring a property for the selected index of internal document status
        /// </summary>
        public string SelectedInternalDocumentRegistationStatus
        {
            get => selectedInternalDocumentRegistationStatus;
            set
            {
                selectedInternalDocumentRegistationStatus = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentRegistationStatus));
                int index = 0;
                for (; index < InternalDocumentRegistationStatus.Count; index++)
                {
                    if (value.Equals(InternalDocumentRegistationStatus[index]))
                    {
                        SelectedInternalDocumentRegistationStatusIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                         if (!string.IsNullOrEmpty(SearchString))
                        {
                            GetDocumentBySearchString(SearchString);
                        }
                        else
                        {
                            GetDocumentsBySendingStatus(SelectedInternalDocumentRegistationStatus);
                        }
                }
            }
        }
        /// <summary>
        /// Declaring a property for an observablecollection of internal document types
        /// </summary>
        public ObservableCollection<string> InternalDocumentTypes { get; private set; }
        /// <summary>
        /// Declaring a variable for the selected index of internal document type
        /// </summary>
        private int selectedInternalDocumentTypeIndex;
        /// <summary>
        /// Declaring a property for the selected index of internal document type
        /// </summary>
        public int SelectedInternalDocumentTypeIndex
        {
            get => selectedInternalDocumentTypeIndex;
            set
            {
                selectedInternalDocumentTypeIndex = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentTypeIndex));
            }
        }
        /// <summary>
        /// Declaring a variable for the selected internal document type
        /// </summary>
        private string selectedInternalDocumentType;
        /// <summary>
        /// Declaring a property for the selected internal document type
        /// </summary>
        public string SelectedInternalDocumentType
        {
            get => selectedInternalDocumentType;
            set
            {
                selectedInternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentType));
                int index = 0;
                for (; index < InternalDocumentTypes.Count; index++)
                {
                    if (value.Equals(InternalDocumentTypes[index]))
                    {
                        SelectedInternalDocumentTypeIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                    else
                    {
                        GetDocumentsBySendingStatus(SelectedInternalDocumentRegistationStatus);
                    }
                }
            }
        }
        /// <summary>
        /// Declaring a variable for the search string
        /// </summary>
        private string searchString;
        /// <summary>
        /// Declaring a property for the search string
        /// </summary>
        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }
        /// <summary>
        /// Declaring a boolean variable to check an internal document
        /// </summary>
        private bool IsInternalDocuments = false;
        /// <summary>
        /// Declaring a variable for the text block title
        /// </summary>
        private string textBlockTitle = "Зарегистрированные поступившие документы";
        /// <summary>
        /// Declaring a property for the text block title
        /// </summary>
        public string TextBlockTitle
        {
            get => textBlockTitle;
            set
            {
                textBlockTitle = value;
                OnPropertyChanged(nameof(TextBlockTitle));
            }
        }
        /// <summary>
        /// Declaring a variable for the contents of the search string
        /// </summary>
        private string searchStringContent = "Поиск по наименованию документа или наименованию контрагента...";
        /// <summary>
        /// Declaring a property for the contents of the search string
        /// </summary>
        public string SearchStringContent
        {
            get => searchStringContent;
            set
            {
                searchStringContent = value;
                OnPropertyChanged(nameof(SearchStringContent));
            }
        }
        /// <summary>
        /// DocumentRegistrationViewModel constructor
        /// </summary>
        /// <param name="window"></param>
        /// <param name="currentUserID"></param>
        public DocumentRegistrationViewModel(DocumentRegistrationWindow window, int currentUserID)
        {
            DocumentRegistrationWindow = window;
            InitializeCurrentUser(currentUserID);
            Employees = new List<Employee>();
            ContractorCompanies = new List<ContractorCompany>();
            Departments = new List<Department>();
            ExternalDocumentList = new List<ExternalDocument>();
            ExternalDocumentsCollection = new ObservableCollection<ExternalDocument>();
            InternalDocumentRegistationStatus = new ObservableCollection<string>();
            ExternalDocumentTypes = new ObservableCollection<string>();
            InternalDocumentList = new List<InternalDocument>();
            InternalDocumentsCollection = new ObservableCollection<InternalDocument>();
            InternalDocumentTypes = new ObservableCollection<string>();
            InitializeDocumentRegistrationStatus();
            GetDocumentsBySendingStatus(SelectedInternalDocumentRegistationStatus);
            InitializeDocumentTypes();
            InitializeInternalDocumentTypes();
            GetAllDepartments();
            GetAllEmployees();
            GetAllContractorCompanyes();
            GetAllDocuments();
            InitializeDocuments();
            GetAllInternalDocuments();
            InitializeInternalDocuments();
        }
        /// <summary>
        /// Function to get and initialize the current user by ID
        /// </summary>
        /// <param name="currentUserID"></param>
        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            try
            {
                EmployesService employesService = new EmployesService();
                currentUser = employesService.GetEmployeeByID(currentUserID).Result;
                Greating = $"Добрый день, {currentUser.EmployeeFirstMiddleName}!";
            }
            catch
            {
                MessageBox.Show("Внимание! Сервер в текущее время не доступен. Попробуйте зайти в приложение позже");
                DocumentRegistrationWindow.Close();
            }
        }
        /// <summary>
        /// Function for initializing the observable collection of internal document registration statuses
        /// </summary>
        private void InitializeDocumentRegistrationStatus()
        {
            InternalDocumentRegistationStatus = new ObservableCollection<string>
            {
                "Все внутренние документы",
                "Внутренние документы не отправленные на рассмотрение",
                "Внутренние документы отправленные на рассмотрение"
            };
            SelectedInternalDocumentRegistationStatusIndex = 0;
            SelectedInternalDocumentRegistationStatus = InternalDocumentRegistationStatus.FirstOrDefault();
        }
        /// <summary>
        /// Function to get and initialize an observable collection of external document types
        /// </summary>
        private void InitializeDocumentTypes()
        {
            ExternalDocumentTypes.Clear();
            ExternalDocumentTypes.Add("Все документы");
            var documentTypes = Enum.GetValues(typeof(ExternalDocumentType));
            foreach (var type in documentTypes)
            {
                ExternalDocumentTypes.Add(ExternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedExternalDocumentType = ExternalDocumentTypes.FirstOrDefault();
        }
        /// <summary>
        /// Function to get and initialize an observable collection of internal document types
        /// </summary>
        private void InitializeInternalDocumentTypes()
        {
            InternalDocumentTypes.Clear();
            InternalDocumentTypes.Add("Все документы");
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentType));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(InternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedInternalDocumentType = InternalDocumentTypes.FirstOrDefault();
        }
        /// <summary>
        /// Function for getting and initializing a list of employees
        /// </summary>
        private void GetAllEmployees()
        {
            Employees.Clear();
            EmployesService employesService = new EmployesService();
            Employees = employesService.GetAllAvailableEmployees().Result;
            if (Departments.Count > 0)
            {
                foreach (Employee employee in Employees)
                {
                    Department department = Departments.Where(d => d.DepartmentID == employee.DepartmentID).FirstOrDefault();
                    employee.Department = department;
                }
            }
        }
        /// <summary>
        /// Function for getting and initializing a list of contractor companyes
        /// </summary>
        private void GetAllContractorCompanyes()
        {
            ContractorCompanies.Clear();
            ContractorCompanyService contractorCompanyService = new ContractorCompanyService();
            ContractorCompanies = contractorCompanyService.GetContractorCompanies().Result;
        }
        /// <summary>
        /// Function for getting and initializing a list of departments
        /// </summary>
        private void GetAllDepartments()
        {
            Departments.Clear();
            DepartmentService departmentService = new DepartmentService();
            Departments = departmentService.GetAllDepartments().Result;
        }
        /// <summary>
        /// Function for getting and initializing a list of external documents
        /// </summary>
        private void GetAllDocuments()
        {
            ExternalDocumentList.Clear();
            ExternalDocumentsService externalDocumentsService = new ExternalDocumentsService();
            ExternalDocumentList = externalDocumentsService.GetAllExternalDocuments().Result;
        }

        /// <summary>
        /// Function to initialize an observable collection of external documents
        /// </summary>
        private void InitializeDocuments()
        {
            ExternalDocumentsCollection.Clear();
            if (ExternalDocumentList != null)
            {
                ExternalDocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                foreach (ExternalDocument document in ExternalDocumentList)
                {
                    Employee employee = Employees.Where(e => e.EmployeeID == document.ReceivingEmployeeID).FirstOrDefault();
                    ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                    document.ReceivingEmployee = employee;
                    document.ContractorCompany = contractorCompany;
                    ExternalDocumentsCollection.Add(document);
                }
            }
        }
        /// <summary>
        /// Function to get a list of internal documents
        /// </summary>
        private void GetAllInternalDocuments()
        {
            InternalDocumentList.Clear();
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            InternalDocumentList = internalDocumentsService.GetInternalDocuments().Result;
        }
        /// <summary>
        /// Function to initialize the observable collection of internal documents
        /// </summary>
        private void InitializeInternalDocuments()
        {
            InternalDocumentsCollection.Clear();
            if (InternalDocumentList != null)
            {
                InternalDocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                foreach (InternalDocument internalDocument in InternalDocumentList)
                {
                    Employee signatory = Employees.Where(e => e.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                    internalDocument.Signatory = signatory;
                    Employee approvedManager = Employees.Where(e => e.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                    internalDocument.ApprovedManager = approvedManager;
                    Employee employeeRecivedDocument = Employees.Where(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                    internalDocument.EmployeeRecievedDocument = employeeRecivedDocument;
                    InternalDocumentsCollection.Add(internalDocument);
                }
            }
        }
        /// <summary>
        /// Announcement of the command to show external documents
        /// </summary>
        public ICommand IShowExternalDocuments => new RelayCommand(showExternalDocuments => ShowExternalDocuments());
        /// <summary>
        /// Function to show external documents
        /// </summary>
        private void ShowExternalDocuments()
        {
            DocumentRegistrationWindow.ExternalDocuments.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.InternalDocuments.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.ComboBoxExternalDocumentTypes.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.ComboBoxInternalDocumentTypes.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.ExternalDocumentTitle.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.SelectInternalDocumentStatus.Visibility = System.Windows.Visibility.Hidden;
            IsInternalDocuments = false;
            SearchString = string.Empty;
            SelectedInternalDocumentType = "Все документы";
            GetDocumentsByDocumentType();
            SearchStringContent = "Поиск по наименованию документа или наименованию контрагента...";
        }
        /// <summary>
        /// Announcement of the command to show internal documents
        /// </summary>
        public ICommand IShowInternalDocuments => new RelayCommand(showInternalDocuments => ShowInternalDocuments());
        /// <summary>
        /// Function to show internal documents
        /// </summary>
        private void ShowInternalDocuments()
        {
            DocumentRegistrationWindow.ExternalDocuments.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.InternalDocuments.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.ComboBoxExternalDocumentTypes.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.ComboBoxInternalDocumentTypes.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.ExternalDocumentTitle.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.SelectInternalDocumentStatus.Visibility = System.Windows.Visibility.Visible;
            IsInternalDocuments = true;
            SearchString = string.Empty;
            SelectedExternalDocumentType = "Все документы";
            GetDocumentsByDocumentType();
            SearchStringContent = "Поиск по инициатору/подписанту документа...";
            SelectedInternalDocumentRegistationStatus = InternalDocumentRegistationStatus.FirstOrDefault();
            //GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
        }
        /// <summary>
        /// Function to get a list of documents by type
        /// </summary>
        private void GetDocumentsByDocumentType()
        {
            if (!IsInternalDocuments)
            {
                if (SelectedExternalDocumentType.Equals("Все документы"))
                {
                    InitializeDocuments();
                }
                else
                {
                    ExternalDocumentsCollection.Clear();
                    if (ExternalDocumentList != null)
                    {
                        ExternalDocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                        foreach (ExternalDocument document in ExternalDocumentList)
                        {
                            ExternalDocumentType documentType = ExternalDocumentTypeConverter.ConvertToEnum(SelectedExternalDocumentType);
                            if (document.ExternalDocumentType == documentType)
                            {
                                Employee employee = Employees.Where(e => e.EmployeeID == document.ReceivingEmployeeID).FirstOrDefault();
                                ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                                document.ReceivingEmployee = employee;
                                document.ContractorCompany = contractorCompany;
                                ExternalDocumentsCollection.Add(document);
                            }
                        }
                    }
                }
            }
            else
            {
                if (SelectedInternalDocumentType.Equals("Все документы"))
                {
                    InitializeInternalDocuments();
                }
                else
                {
                    InternalDocumentsCollection.Clear();
                    if (InternalDocumentList != null)
                    {
                        InternalDocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                        foreach (InternalDocument internalDocument in InternalDocumentList)
                        {
                            InternalDocumentType internalDocumentType = InternalDocumentTypeConverter.ConvertToEnum(SelectedInternalDocumentType);
                            if (internalDocument.InternalDocumentType == internalDocumentType)
                            {
                                Employee signatory = Employees.Where(e => e.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                                internalDocument.Signatory = signatory;
                                Employee approvedManager = Employees.Where(e => e.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                                internalDocument.ApprovedManager = approvedManager;
                                Employee employeeRecivedDocument = Employees.Where(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                                internalDocument.EmployeeRecievedDocument = employeeRecivedDocument;
                                InternalDocumentsCollection.Add(internalDocument);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Function for obtaining the status of an internal document
        /// </summary>
        /// <param name="selectedInternalDocumentSendingStatus"></param>
        private void GetDocumentsBySendingStatus(string selectedInternalDocumentSendingStatus)
        {
            if (!IsInternalDocuments) return;
            if(SelectedInternalDocumentRegistationStatus.Equals("Все внутренние документы"))
            {
                GetDocumentsByDocumentType();
            }
            else
            {
                GetDocumentsByDocumentType();
                List<InternalDocument> internalDocuments = new List<InternalDocument>();
                if (SelectedInternalDocumentRegistationStatus.Equals("Внутренние документы не отправленные на рассмотрение")) 
                {
                    internalDocuments = InternalDocumentsCollection.Where(r => r.EmployeeRecievedDocument == null).ToList();
                    InternalDocumentsCollection.Clear();
                    foreach (InternalDocument document in internalDocuments)
                    {
                        InternalDocumentsCollection.Add(document);
                    }
                }
                else
                {
                    internalDocuments = InternalDocumentsCollection.Where(r => r.EmployeeRecievedDocument != null).ToList();
                    InternalDocumentsCollection.Clear();
                    foreach (InternalDocument document in internalDocuments)
                    {
                        InternalDocumentsCollection.Add(document);
                    }
                }
            }
        }
        /// <summary>
        /// Function to get a list of internal documents by search string
        /// </summary>
        /// <param name="searchingString"></param>
        public void GetDocumentBySearchString(string searchingString)
        {
            if (!IsInternalDocuments)
            {
                if (string.IsNullOrEmpty(searchingString))
                {
                    ExternalDocumentsCollection.Clear();
                    GetDocumentsByDocumentType();
                    return;
                }
                GetDocumentsByDocumentType();
                List<ExternalDocument> documents = ExternalDocumentsCollection.ToList();
                ExternalDocumentsCollection.Clear();
                if (documents != null)
                {
                    documents.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                    foreach (ExternalDocument document in documents)
                    {
                        Employee employee = Employees.Where(e => e.EmployeeID == document.ReceivingEmployeeID).FirstOrDefault();
                        ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                        document.ReceivingEmployee = employee;
                        document.ContractorCompany = contractorCompany;
                        if (document.ExternalDocumentTitle.ToLower().Contains(searchingString.ToLower()))
                        {
                            ExternalDocumentsCollection.Add(document);
                        }
                    }
                    if (ExternalDocumentsCollection.Count == 0)
                    {
                        foreach (ExternalDocument document in documents)
                        {
                            if (document.ContractorCompany.ContractorCompanyTitle.ToLower().Contains(searchingString.ToLower()))
                            {
                                ExternalDocumentsCollection.Add(document);
                            }
                        }
                    }
                    if (ExternalDocumentsCollection.Count == 0)
                    {
                        foreach (ExternalDocument document in documents)
                        {
                            if (document.ExternalDocumentNumber != null && document.ExternalDocumentNumber.ToLower().Contains(searchingString.ToLower()))
                            {
                                ExternalDocumentsCollection.Add(document);
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchingString))
                {
                    GetDocumentsBySendingStatus(SelectedInternalDocumentRegistationStatus);
                    return;
                }
                GetDocumentsBySendingStatus(SelectedInternalDocumentRegistationStatus);
                List<InternalDocument> internalDocuments = InternalDocumentsCollection.ToList();
                InternalDocumentsCollection.Clear();
                if (internalDocuments != null)
                {
                    internalDocuments.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                    foreach (InternalDocument internalDocument in internalDocuments)
                    {
                        Employee signatory = Employees.Where(e => e.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                        internalDocument.Signatory = signatory;
                        Employee approvedManager = Employees.Where(e => e.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                        internalDocument.ApprovedManager = approvedManager;
                        Employee employeeRecivedDocument = Employees.Where(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                        internalDocument.EmployeeRecievedDocument = employeeRecivedDocument;
                        if (internalDocument.Signatory.EmployeeFullName.ToLower().Contains(searchingString.ToLower()))
                        {
                            InternalDocumentsCollection.Add(internalDocument);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Announcement of the command to create a new document
        /// </summary>
        public ICommand ICreateNewDocument => new RelayCommand(createNewDocument => CreateNewDocument());
        /// <summary>
        /// Function to create a new document
        /// </summary>
        private void CreateNewDocument()
        {
            if (!IsInternalDocuments)
            {
                OpenDocumentWindow(null);
            }
            else
            {
                OpenInternalDocumentWindow(null);
            }
        }
        /// <summary>
        /// Function to open DocumentWindow for create an external document
        /// </summary>
        /// <param name="document"></param>
        private void OpenDocumentWindow(ExternalDocument document)
        {
            DocumentWindow documentWindow = new DocumentWindow(document);
            documentWindow.ShowDialog();
            GetAllContractorCompanyes();
            GetAllDocuments();
            InitializeDocuments();
        }
        /// <summary>
        /// Function to open InternalDocumentWindow for create an internal document
        /// </summary>
        /// <param name="internalDocument"></param>
        private void OpenInternalDocumentWindow(InternalDocument internalDocument)
        {
            InternalDocumentWindow internalDocumentWindow = new InternalDocumentWindow(internalDocument);
            internalDocumentWindow.ShowDialog();
            GetAllInternalDocuments();
            InitializeInternalDocuments();
            if (string.IsNullOrEmpty(SearchString))
            {
                GetDocumentsBySendingStatus(SelectedInternalDocumentRegistationStatus);
                return;
            }
            GetDocumentBySearchString(SearchString);
        }
        /// <summary>
        /// Announcement of the command to open ManagerPanelWindow
        /// </summary>
        public ICommand IOpenManagerPanelWindow => new RelayCommand(openManagerPanelWindow => OpenManagerPanelWindow());
        /// <summary>
        /// Function to open ManagerPanelWindow
        /// </summary>
        private void OpenManagerPanelWindow()
        {
            ManagerPanelWindow managerPanelWindow = new ManagerPanelWindow(currentUser.EmployeeID);
            managerPanelWindow.Show();
        }
        /// <summary>
        /// Announcement of the command to close DocumentRegistrationWindow
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => { DocumentRegistrationWindow.Close(); });
    }
}

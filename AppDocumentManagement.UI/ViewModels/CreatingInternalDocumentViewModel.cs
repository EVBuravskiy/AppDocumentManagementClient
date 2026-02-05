using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.InternalDocumentService.Services;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ViewModel for CreatingInternalDocumentWindow
    /// </summary>
    public class CreatingInternalDocumentViewModel : BaseViewModelClass
    {
        /// <summary>
        /// CreatingInternalDocumentWindow announcement
        /// </summary>
        private CreatingInternalDocumentWindow CreatingInternalDocumentWindow { get; set; }
        /// <summary>
        /// Declaration of the current user variable
        /// </summary>
        private Employee currentUser;
        /// <summary>
        /// File handling service announcement
        /// </summary>
        private IFileDialogService fileDialogService;
        /// <summary>
        /// Declaring a variable for the user who signed the document
        /// </summary>
        private Employee CurrentSignatory;
        /// <summary>
        /// Property declaration for a list of internal document types
        /// </summary>
        public List<string> InternalDocumentTypes { get; set; }
        /// <summary>
        /// Declaring a variable for the type of the selected internal document
        /// </summary>
        private InternalDocumentType selectedInternalDocumentType;
        /// <summary>
        /// Property declaration for the selected internal document type
        /// </summary>
        public InternalDocumentType SelectedInternalDocumentType
        {
            get => selectedInternalDocumentType;
            set
            {
                selectedInternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentType));
            }
        }
        /// <summary>
        /// Declaring an index for the selected internal document type
        /// </summary>
        private int selectedInternalDocumentTypeIndex;
        /// <summary>
        /// Declaring a property for the index of the selected internal document type
        /// </summary>
        public int SelectedInternalDocumentTypeIndex
        {
            get => selectedInternalDocumentTypeIndex;
            set
            {
                if (selectedInternalDocumentTypeIndex != value)
                {
                    selectedInternalDocumentTypeIndex = value;
                    OnPropertyChanged(nameof(SelectedInternalDocumentTypeIndex));
                    SelectedInternalDocumentType = InternalDocumentTypeConverter.BackConvert(value);
                }
            }
        }
        /// <summary>
        /// Declaring a variable for the date of an internal document
        /// </summary>
        private DateTime internalDocumentDate;
        /// <summary>
        /// Declaring a property for the date of an internal document
        /// </summary>
        public DateTime InternalDocumentDate
        {
            get => internalDocumentDate;
            set
            {
                internalDocumentDate = value;
                OnPropertyChanged(nameof(InternalDocumentDate));
            }
        }
        /// <summary>
        /// Declaring a variable for the title of an internal document
        /// </summary>
        private string internalDocumentTitle;
        /// <summary>
        /// Declaring a property for the title of an internal document
        /// </summary>
        public string InternalDocumentTitle
        {
            get => internalDocumentTitle;
            set
            {
                internalDocumentTitle = value;
                OnPropertyChanged(nameof(InternalDocumentTitle));
            }
        }
        /// <summary>
        /// Declaring a variable for the content of an internal document
        /// </summary>
        private string internalDocumentContent;
        /// <summary>
        /// Declaring a property for the content of an internal document
        /// </summary>
        public string InternalDocumentContent
        {
            get => internalDocumentContent;
            set
            {
                internalDocumentContent = value;
                OnPropertyChanged(nameof(InternalDocumentContent));
            }
        }
        /// <summary>
        /// Declaring a property for the InternalDocumentFile observable collection
        /// </summary>
        public ObservableCollection<InternalDocumentFile> InternalDocumentFiles { get; set; }
        /// <summary>
        /// Declaring a property for the selected InternalDocumentFile
        /// </summary>
        public InternalDocumentFile SelectedInternalDocumentFile { get; set; }

        /// <summary>
        /// CreatingInternalDocumentViewModel constructor
        /// </summary>
        /// <param name="window"></param>
        /// <param name="currentUserID"></param>
        public CreatingInternalDocumentViewModel(CreatingInternalDocumentWindow window, int currentUserID)
        {
            CreatingInternalDocumentWindow = window;
            InitializeCurrentUser(currentUserID);
            CurrentSignatory = null;
            fileDialogService = new WindowsDialogService();
            InternalDocumentTypes = new List<string>();
            InitializeInternalDocumentTypes();
            InternalDocumentDate = DateTime.Now;
            InternalDocumentFiles = new ObservableCollection<InternalDocumentFile>();
        }
        /// <summary>
        /// Function to initialize current user
        /// </summary>
        /// <param name="currentUserID"></param>
        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployesService employeesService = new EmployesService();
            currentUser = employeesService.GetEmployeeByID(currentUserID).Result;
        }
        /// <summary>
        /// Function for initializing internal document types
        /// </summary>
        private void InitializeInternalDocumentTypes()
        {
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentType));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(InternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedInternalDocumentType = InternalDocumentTypeConverter.ConvertToEnum(InternalDocumentTypes.FirstOrDefault());
            SelectedInternalDocumentTypeIndex = 0;
        }
        /// <summary>
        /// Declaring a command to add an internal document file
        /// </summary>
        public ICommand IBrowseInternalDocumentFiles => new RelayCommand(browseInternalDocumentFiles => BrowseInternalDocumentFile());
        /// <summary>
        /// Function to add an internal document file
        /// </summary>
        private void BrowseInternalDocumentFile()
        {
            var filePath = fileDialogService.OpenFile();
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            InternalDocumentFile internalDocumentFile = new InternalDocumentFile();
            internalDocumentFile.FileName = fileName;
            internalDocumentFile.FileExtension = fileExtension;
            internalDocumentFile.FileData = fileData;
            InternalDocumentFiles.Add(internalDocumentFile);
        }
        /// <summary>
        /// Declaring a command to delete an internal document file
        /// </summary>
        public ICommand IDeleteInternalDocumentFile => new RelayCommand(deleteInternalDocumentFile => DeleteInternalDocumentFile());
        /// <summary>
        /// Function to delete an internal document file
        /// </summary>
        private void DeleteInternalDocumentFile()
        {
            if (SelectedInternalDocumentFile != null)
            {
                InternalDocumentFiles.Remove(SelectedInternalDocumentFile);
            }
        }
        /// <summary>
        /// Declaring a command to send an internal document to an employee
        /// </summary>
        public ICommand ISendInternalDocument => new RelayCommand(sendInternalDocument => SendInternalDocument());
        /// <summary>
        /// Function to send an internal document to an employee
        /// </summary>
        private void SendInternalDocument()
        {
            InternalDocument newInternalDocument = CreateInternalDocument();
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true, currentUser.Department);
            examiningPersonsWindow.ShowDialog();
            Employee sendingEmployee = examiningPersonsWindow.viewModel.SelectedEmployee;
            if (sendingEmployee == null)
            {
                MessageBox.Show($"Ошибка! Невозможно направить документ выбранному лицу");
                return;
            }
            newInternalDocument.EmployeeRecievedDocumentID = sendingEmployee.EmployeeID;
            newInternalDocument.InternalDocumentSendingDate = DateTime.Now;
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            bool result = internalDocumentsService.AddInternalDocument(newInternalDocument).Result;
            if (result)
            {
                MessageBox.Show($"Документ {newInternalDocument.InternalDocumentTitle} зарегистрирован\nи отправлен на рассмотрение {sendingEmployee.EmployeeFullName}");
                CreatingInternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show($"Ошибка в сохранении созданного внутреннего документа");
            }
        }
        /// <summary>
        /// Declaring a command to register an internal document
        /// </summary>
        public ICommand IRegisterInternalDocument => new RelayCommand(registerInternalDocument => RegisterInternalDocument());
        /// <summary>
        /// Function to register an internal document
        /// </summary>
        private void RegisterInternalDocument()
        {
            InternalDocument newInternalDocument = CreateInternalDocument();
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            bool result = internalDocumentsService.AddInternalDocument(newInternalDocument).Result;
            if (result)
            {
                MessageBox.Show($"Документ {newInternalDocument.InternalDocumentTitle} зарегистрирован");
                CreatingInternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show($"Ошибка в сохранении созданного внутреннего документа");
            }
        }
        /// <summary>
        /// Function for creating a new internal document
        /// </summary>
        /// <returns>InternalDocument</returns>
        private InternalDocument CreateInternalDocument()
        {
            InternalDocument newInternalDocument = new InternalDocument();
            newInternalDocument.InternalDocumentTitle = InternalDocumentTitle;
            newInternalDocument.InternalDocumentContent = InternalDocumentContent;
            newInternalDocument.InternalDocumentDate = InternalDocumentDate;
            newInternalDocument.InternalDocumentType = SelectedInternalDocumentType;
            newInternalDocument.InternalDocumentStatus = DocumentStatus.UnderConsideration;
            newInternalDocument.SignatoryID = currentUser.EmployeeID;
            newInternalDocument.InternalDocumentRegistrationNumber = GetInternalDocumentNumber();
            newInternalDocument.RegistrationDate = DateTime.Now;
            newInternalDocument.IsRegistered = true;
            if (InternalDocumentFiles.Count > 0)
            {
                newInternalDocument.InternalDocumentFiles = InternalDocumentFiles.ToList();
            }
            return newInternalDocument;
        }
        /// <summary>
        /// Function for obtaining the internal document number
        /// </summary>
        /// <returns>string</returns>
        private string GetInternalDocumentNumber()
        {
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            string type = InternalDocumentTypeConverter.ConvertToString(SelectedInternalDocumentType).Substring(0, 2);
            int number = internalDocumentsService.GetCountInternalDocumentByType(SelectedInternalDocumentType) + 1;
            return $"{number}/{type}";
        }
        /// <summary>
        /// Command for close CreatingInternalDocumentWindow
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => { CreatingInternalDocumentWindow.Close(); });
    }
}

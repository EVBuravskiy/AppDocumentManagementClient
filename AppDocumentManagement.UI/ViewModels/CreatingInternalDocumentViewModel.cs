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
    public class CreatingInternalDocumentViewModel : BaseViewModelClass
    {
        private CreatingInternalDocumentWindow CreatingInternalDocumentWindow { get; set; }
        private Employee currentUser;
        private IFileDialogService fileDialogService;
        private Employee CurrentSignatory;
        public List<string> InternalDocumentTypes { get; set; }

        private InternalDocumentType selectedInternalDocumentType;

        public InternalDocumentType SelectedInternalDocumentType
        {
            get => selectedInternalDocumentType;
            set
            {
                selectedInternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentType));
            }
        }

        private int selectedInternalDocumentTypeIndex;
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
        private DateTime internalDocumentDate;
        public DateTime InternalDocumentDate
        {
            get => internalDocumentDate;
            set
            {
                internalDocumentDate = value;
                OnPropertyChanged(nameof(InternalDocumentDate));
            }
        }

        private string internalDocumentTitle;
        public string InternalDocumentTitle
        {
            get => internalDocumentTitle;
            set
            {
                internalDocumentTitle = value;
                OnPropertyChanged(nameof(InternalDocumentTitle));
            }
        }

        private string internalDocumentContent;
        public string InternalDocumentContent
        {
            get => internalDocumentContent;
            set
            {
                internalDocumentContent = value;
                OnPropertyChanged(nameof(InternalDocumentContent));
            }
        }

        public ObservableCollection<InternalDocumentFile> InternalDocumentFiles { get; set; }

        public InternalDocumentFile SelectedInternalDocumentFile { get; set; }


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

        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployesService employeesService = new EmployesService();
            currentUser = employeesService.GetEmployeeByID(currentUserID).Result;
        }

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

        public ICommand IBrowseInternalDocumentFiles => new RelayCommand(browseInternalDocumentFiles => BrowseInternalDocumentFile());
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

        public ICommand IDeleteInternalDocumentFile => new RelayCommand(deleteInternalDocumentFile => DeleteInternalDocumentFile());
        private void DeleteInternalDocumentFile()
        {
            if (SelectedInternalDocumentFile != null)
            {
                InternalDocumentFiles.Remove(SelectedInternalDocumentFile);
            }
        }

        public ICommand ISendInternalDocument => new RelayCommand(sendInternalDocument => SendInternalDocument());

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

        public ICommand IRegisterInternalDocument => new RelayCommand(registerInternalDocument => RegisterInternalDocument());
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
        
        private string GetInternalDocumentNumber()
        {
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            string type = InternalDocumentTypeConverter.ConvertToString(SelectedInternalDocumentType).Substring(0, 2);
            int number = internalDocumentsService.GetCountInternalDocumentByType(SelectedInternalDocumentType) + 1;
            return $"{number}/{type}";
        }
        public ICommand IExit => new RelayCommand(exit => { CreatingInternalDocumentWindow.Close(); });
    }
}

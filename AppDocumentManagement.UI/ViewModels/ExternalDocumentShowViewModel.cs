using AppDocumentManagement.ExternalDocumentService.Services;
using AppDocumentManagement.InternalDocumentService.Services;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace AppDocumentManagement.UI.ViewModels
{
    public class ExternalDocumentShowViewModel : BaseViewModelClass
    {
        ExternalDocumentShowWindow ExternalDocumentShowWindow;

        ExternalDocument ExternalDocument;

        Employee CurrentEmployee;

        private IFileDialogService fileDialogService;

        private string documentType;
        public string DocumentType
        {
            get => documentType;
            set
            {
                documentType = value;
                OnPropertyChanged(nameof(DocumentType));
            }
        }

        private string documentNumber;
        public string DocumentNumber
        {
            get => documentNumber;
            set
            {
                documentNumber = value;
                OnPropertyChanged(nameof(DocumentNumber));
            }
        }

        private string documentTitle;
        public string DocumentTitle
        {
            get => documentTitle;
            set
            {
                documentTitle = value;
                OnPropertyChanged(nameof(DocumentTitle));
            }
        }

        private string textBlockCompanyTitle;
        public string TextBlockCompanyTitle
        {
            get => textBlockCompanyTitle;
            set
            {
                textBlockCompanyTitle = value;
                OnPropertyChanged(nameof(TextBlockCompanyTitle));
            }
        }

        private string textBlockCompanyShortTitle;
        public string TextBlockCompanyShortTitle
        {
            get => textBlockCompanyShortTitle;
            set
            {
                textBlockCompanyShortTitle = value;
                OnPropertyChanged(nameof(TextBlockCompanyShortTitle));
            }
        }

        private string textBlockCompanyAddress;
        public string TextBlockCompanyAddress
        {
            get => textBlockCompanyAddress;
            set
            {
                textBlockCompanyAddress = value;
                OnPropertyChanged(nameof(TextBlockCompanyAddress));
            }
        }
        private string textBlockCompanyPhone;
        public string TextBlockCompanyPhone
        {
            get => textBlockCompanyPhone;
            set
            {
                textBlockCompanyPhone = value;
                OnPropertyChanged(nameof(TextBlockCompanyPhone));
            }
        }

        private string textBlockCompanyEmail;
        public string TextBlockCompanyEmail
        {
            get => textBlockCompanyEmail;
            set
            {
                textBlockCompanyEmail = value;
                OnPropertyChanged(nameof(TextBlockCompanyEmail));
            }
        }

        private List<ExternalDocumentFile> ExternalDocumentFilesList;

        public ObservableCollection<ExternalDocumentFile> ExternalDocumentFiles { get; set; }

        private ExternalDocumentFile selectedExternalDocumentFile;
        public ExternalDocumentFile SelectedExternalDocumentFile
        {
            get => selectedExternalDocumentFile;
            set
            {
                selectedExternalDocumentFile = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentFile));
                if (value != null)
                {
                    BrowseToSaveExternalDocumentFile();
                }
            }
        }

        public ExternalDocumentShowViewModel(ExternalDocumentShowWindow externalDocumentShowWindow, Employee currentEmployee, ExternalDocument inputExternalDocument, ContractorCompany documentContractorCompany)
        {
            ExternalDocumentShowWindow = externalDocumentShowWindow;
            CurrentEmployee = currentEmployee;
            EmployeeRole role = currentEmployee.EmployeeRole;
            fileDialogService = new WindowsDialogService();
            ExternalDocument = inputExternalDocument;
            ExternalDocument.ContractorCompany = documentContractorCompany;
            ExternalDocumentFilesList = new List<ExternalDocumentFile>();
            ExternalDocumentFiles = new ObservableCollection<ExternalDocumentFile>();
            if (inputExternalDocument != null)
            {
                DocumentType = ExternalDocumentTypeConverter.ConvertToString(ExternalDocument.ExternalDocumentType);
                DocumentNumber = inputExternalDocument.ExternalDocumentNumber;
                DocumentTitle = inputExternalDocument.ExternalDocumentTitle;
                if (documentContractorCompany != null)
                {
                    TextBlockCompanyTitle = documentContractorCompany.ContractorCompanyTitle;
                    TextBlockCompanyShortTitle = documentContractorCompany.ContractorCompanyShortTitle;
                    TextBlockCompanyAddress = $"Юридический адрес: {documentContractorCompany.ContractorCompanyAddress}";
                    TextBlockCompanyPhone = $"Контактный телефон: {documentContractorCompany.ContractorCompanyPhone}";
                    TextBlockCompanyEmail = $"Адрес электронной почты: {documentContractorCompany.ContractorCompanyEmail}";
                }
                GetExternalDocumentFiles();
                InitializeExternalDocumentFiles();
                if(role == EmployeeRole.Performer)
                {
                    ExternalDocumentShowWindow.ExternalDocumentFiles.Height = new GridLength(420, GridUnitType.Pixel);
                    ExternalDocumentShowWindow.ExternalDocumentButtons.Height = new GridLength(0, GridUnitType.Pixel);
                }
            }
        }

        private void GetExternalDocumentFiles()
        {
            ExternalDocumentFilesList.Clear();
            ExternalDocumentFileService externalDocumentFileService = new ExternalDocumentFileService();
            ExternalDocumentFilesList = externalDocumentFileService.GetExternalDocumentFiles(ExternalDocument.ExternalDocumentID).Result;
        }

        public void InitializeExternalDocumentFiles()
        {
            ExternalDocumentFiles.Clear();
            if (ExternalDocumentFilesList.Count > 0)
            {
                foreach (ExternalDocumentFile file in ExternalDocumentFilesList)
                {
                    ExternalDocumentFiles.Add(file);
                }
            }
        }

        public ICommand IBrowseToSaveExternalDocumentFile => new RelayCommand(browseToSaveExternalDocument => BrowseToSaveExternalDocumentFile());
        private void BrowseToSaveExternalDocumentFile()
        {
            var filePath = fileDialogService.SaveFile(SelectedExternalDocumentFile.ExternalFileExtension, SelectedExternalDocumentFile.ExternalFileName);
            bool result = FileProcessing.SaveExternalDocumentFileToPath(filePath, SelectedExternalDocumentFile);
            if (result)
            {
                MessageBox.Show($"Файл {SelectedExternalDocumentFile.ExternalFileName} сохранен");
            }
            else
            {
                MessageBox.Show($"Файл {SelectedExternalDocumentFile.ExternalFileName} уже имеется, либо не был сохранен");
            }
        }

        public ICommand ILoadExternalDocumentFiles => new RelayCommand(loadExternalDocumentFiles => LoadExternalDocumentFiles());
        private void LoadExternalDocumentFiles()
        {
            string directoryPath = string.Empty;
            foreach (ExternalDocumentFile file in ExternalDocumentFiles)
            {
                directoryPath = FileProcessing.SaveExternalDocumentFileFromDB(file, "ExternalDocuments");
            }
            if (string.IsNullOrEmpty(directoryPath))
            {
                MessageBox.Show("Не удалось сохранить файлы");
            }
            else
            {
                DirectoryProcessing.OpenDirectory(directoryPath);
            }
        }

        public ICommand IBrowseExternalDocumentFile => new RelayCommand(browseExternalDocumentFile => BrowseExternalDocumentFile());
        private void BrowseExternalDocumentFile()
        {
            var filePath = fileDialogService.OpenFile("Text files(*.txt)| *.txt | PDF files(*.pdf) | *.pdf | Image files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*");
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            ExternalDocumentFile documentFile = new ExternalDocumentFile();
            documentFile.ExternalFileName = fileName;
            documentFile.ExternalFileExtension = fileExtension;
            documentFile.ExternalFileData = fileData;
            documentFile.ExternalDocument = ExternalDocument;
            ExternalDocumentFileService externalDocumentFileService = new ExternalDocumentFileService();
            bool result = externalDocumentFileService.AddExternalDocumentFile(documentFile).Result;
            if (result) MessageBox.Show("Файл успешно добавлен");
            else MessageBox.Show("Не удалось добавить файл");
            GetExternalDocumentFiles();
            InitializeExternalDocumentFiles();
        }

        public ICommand IAgreeDocument => new RelayCommand(agreeDocument => AgreeDocument());
        private void AgreeDocument()
        {
            bool result = false;
            if (CurrentEmployee != null)
            {
                ExternalDocument.ExternalDocumentStatus = DocumentStatus.Agreed;
                ExternalDocumentsService externalDocumentsService = new ExternalDocumentsService();
                result = externalDocumentsService.UpdateExternalDocument(ExternalDocument).Result;
            }
            if (result)
            {
                MessageBox.Show($"Документ был согласован: {CurrentEmployee.EmployeeFullName}");
                ExternalDocumentShowWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в согласовании документа");
            }
        }

        public ICommand IRefuseDocument => new RelayCommand(refuseDocument => RefuseDocument());
        private void RefuseDocument()
        {
            bool result = false;
            if (CurrentEmployee != null)
            {
                ExternalDocument.ExternalDocumentStatus = DocumentStatus.Refused;
                ExternalDocumentsService externalDocumentsService = new ExternalDocumentsService();
                result = externalDocumentsService.UpdateExternalDocument(ExternalDocument).Result;
            }
            if (result)
            {
                MessageBox.Show($"В согласовании документа было отказано: {CurrentEmployee.EmployeeFullName}");
                ExternalDocumentShowWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в согласовании документа");
            }
        }

        public ICommand ISendToWork => new RelayCommand(sendToWork => SendToWork());

        private void SendToWork()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true);
            examiningPersonsWindow.ShowDialog();
            bool result = false;
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                ExternalDocument.ReceivingEmployee = examiningPersonsWindow.viewModel.SelectedEmployee;
                ExternalDocument.ExternalDocumentSendingDate = DateTime.Now;
                ExternalDocumentsService externalDocumentsService = new ExternalDocumentsService();
                result = externalDocumentsService.UpdateExternalDocument(ExternalDocument).Result;
            }
            if (result)
            {
                MessageBox.Show("Документ успешно направлен для исполнения");
                ExternalDocumentShowWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в отправке документа");
            }
        }

        //TODO: Реализовать создание задачи к документу
        public ICommand ICreateTask;

        public ICommand IExit => new RelayCommand(exit => { ExternalDocumentShowWindow.Close(); });
    }
}

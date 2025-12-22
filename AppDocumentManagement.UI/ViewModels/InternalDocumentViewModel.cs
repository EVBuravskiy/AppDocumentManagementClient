using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.InternalDocumentService.Services;
using AppDocumentManagement.Models;
using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagement.UI.ViewModels
{
    public class InternalDocumentViewModel : BaseViewModelClass
    {
        private InternalDocumentWindow InternalDocumentWindow;
        private InternalDocument InternalDocument;
        private IFileDialogService fileDialogService;
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
        private Employee approvedManager;
        public Employee ApprovedManager
        {
            get => approvedManager;
            set
            {
                approvedManager = value;
                OnPropertyChanged(nameof(ApprovedManager));
            }
        }

        private string approvedManagerDepartment;
        public string ApprovedManagerDepartment
        {
            get => approvedManagerDepartment;
            set
            {
                approvedManagerDepartment = value;
                OnPropertyChanged(nameof(ApprovedManagerDepartment));
            }
        }
        private string approvedManagerPosition;
        public string ApprovedManagerPosition
        {
            get => approvedManagerPosition;
            set
            {
                approvedManagerPosition = value;
                OnPropertyChanged(nameof(ApprovedManagerPosition));
            }
        }
        private string approvedManagerFullName;
        public string ApprovedManagerFullName
        {
            get => approvedManagerFullName;
            set
            {
                approvedManagerFullName = value;
                OnPropertyChanged(nameof(ApprovedManagerFullName));
            }
        }

        private Employee signatory;
        public Employee Signatory
        {
            get => signatory;
            set
            {
                signatory = value;
                OnPropertyChanged(nameof(Signatory));
            }
        }

        private string signatoryDepartment;
        public string SignatoryDepartment
        {
            get => signatoryDepartment;
            set
            {
                signatoryDepartment = value;
                OnPropertyChanged(nameof(SignatoryDepartment));
            }
        }
        private string signatoryPosition;
        public string SignatoryPosition
        {
            get => signatoryPosition;
            set
            {
                signatoryPosition = value;
                OnPropertyChanged(nameof(SignatoryPosition));
            }
        }
        private string signatoryFullName;
        public string SignatoryFullName
        {
            get => signatoryFullName;
            set
            {
                signatoryFullName = value;
                OnPropertyChanged(nameof(SignatoryFullName));
            }
        }

        private Employee EmployeeRecievedDocument { get; set; }
        private List<InternalDocumentFile> InternalDocumentFilesList { get; set; }

        public ObservableCollection<InternalDocumentFile> InternalDocumentFiles { get; set; }

        public InternalDocumentFile SelectedInternalDocumentFile { get; set; }

        private List<Department> Departments;

        private string registerOrUpdateBtnTitle = "Зарегистрировать";
        public string RegisterOrUpdateBtnTitle
        {
            get => registerOrUpdateBtnTitle;
            set
            {
                registerOrUpdateBtnTitle = value;
                OnPropertyChanged(nameof(RegisterOrUpdateBtnTitle));
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


        public InternalDocumentViewModel(InternalDocumentWindow internalDocumentWindow, InternalDocument internalDocument = null)
        {
            InternalDocumentWindow = internalDocumentWindow;
            InternalDocument = internalDocument;
            fileDialogService = new WindowsDialogService();
            InternalDocumentDate = DateTime.Now;
            InternalDocumentTypes = new List<string>();
            InternalDocumentFilesList = new List<InternalDocumentFile>();
            InternalDocumentFiles = new ObservableCollection<InternalDocumentFile>();
            Departments = new List<Department>();
            InitializeInternalDocumentTypes();
            GetInternalDocumentFiles();
            InitializeInternalDocumentFiles();
            InitializeDepartments();
            if (InternalDocument != null)
            {
                InternalDocumentWindow.AddSignatoryBtnTitle.Visibility = Visibility.Hidden;
                InternalDocumentWindow.ChangeSignatoryBtnTitle.Visibility = Visibility.Visible;
                RegisterOrUpdateBtnTitle = "Сохранить изменения";
                SelectedInternalDocumentType = InternalDocument.InternalDocumentType;
                SelectedInternalDocumentTypeIndex = InternalDocumentTypeConverter.ToIntConvert(InternalDocument.InternalDocumentType);
                if (InternalDocument.ApprovedManagerID != 0)
                {
                    EmployesService employesService = new EmployesService();
                    ApprovedManager = employesService.GetEmployeeByID(InternalDocument.ApprovedManagerID).Result;
                    if(ApprovedManager != null)
                    {
                        InternalDocumentWindow.AddApproveManagerBtnTitle.Visibility = Visibility.Hidden;
                        InternalDocumentWindow.ChangeApproveManagerBtnTitle.Visibility = Visibility.Visible;
                        DepartmentService departmentService = new DepartmentService();
                        Department department = departmentService.GetDepartmentByID(ApprovedManager.DepartmentID).Result;
                        ApprovedManager.Department = department;
                    }
                    ApprovedManagerDepartment = ApprovedManager.Department.DepartmentTitle;
                    ApprovedManagerPosition = ApprovedManager.Position;
                    ApprovedManagerFullName = ApprovedManager.EmployeeFullName;

                }
                if (InternalDocument.SignatoryID != 0)
                {
                    EmployesService employesService = new EmployesService();
                    Signatory = employesService.GetEmployeeByID(InternalDocument.SignatoryID).Result;
                    if (Signatory != null)
                    {
                        DepartmentService departmentService = new DepartmentService();
                        Department department = departmentService.GetDepartmentByID(Signatory.DepartmentID).Result;
                        Signatory.Department = department;
                        SignatoryDepartment = Signatory.Department.DepartmentTitle;
                        SignatoryPosition = Signatory.Position;
                        SignatoryFullName = Signatory.EmployeeFullName;

                    }
                }
                if(InternalDocument.EmployeeRecievedDocumentID != 0)
                {
                    EmployesService employesService = new EmployesService();
                    EmployeeRecievedDocument = employesService.GetEmployeeByID(InternalDocument.EmployeeRecievedDocumentID).Result;
                }
                InternalDocumentDate = InternalDocument.InternalDocumentDate;
                InternalDocumentTitle = InternalDocument.InternalDocumentTitle;
                InternalDocumentContent = InternalDocument.InternalDocumentContent;
            }
        }

        private void InitializeInternalDocumentTypes()
        {
            InternalDocumentTypes = new List<string>();
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentType));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(InternalDocumentTypeConverter.ConvertToString(type));
            }
        }

        private void GetInternalDocumentFiles()
        {
            if (InternalDocument == null) return;
            InternalDocumentFilesList.Clear();
            InternalDocumentFileService internalDocumentFileService = new InternalDocumentFileService();
            InternalDocumentFilesList = internalDocumentFileService.GetInternalDocumentFiles(InternalDocument.InternalDocumentID).Result;
            SelectedInternalDocumentFile = InternalDocumentFiles.FirstOrDefault();
        }
        private void InitializeInternalDocumentFiles()
        {
            InternalDocumentFiles.Clear();
            if (InternalDocumentFilesList.Count > 0)
            {
                foreach (InternalDocumentFile file in InternalDocumentFilesList)
                {
                    InternalDocumentFiles.Add(file);
                }
            }
        }

        private void InitializeDepartments()
        {
            Departments.Clear();
            DepartmentService departmentService = new DepartmentService();
            Departments = departmentService.GetAllDepartments().Result;
        }

        public ICommand IOpenApprovedManagerWindow => new RelayCommand(openApprovedManagerWindow => OpenApprovedManagerWindow());
        private void OpenApprovedManagerWindow()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true, null);
            examiningPersonsWindow.ShowDialog();
            if(examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                ApprovedManager = examiningPersonsWindow.viewModel.SelectedEmployee;
                InitializeApprovedManagerData();
            }
        }

        private void InitializeApprovedManagerData()
        {
            if (ApprovedManager != null)
            {
                ApprovedManagerDepartment = ApprovedManager.Department.DepartmentTitle;
                ApprovedManagerPosition = ApprovedManager.Position;
                ApprovedManagerFullName = ApprovedManager.EmployeeFullName;
            }
        }

        public ICommand IOpenSignatoryWindow => new RelayCommand(openSignatoryWindow => OpenSignatoryWindow());
        private void OpenSignatoryWindow()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(false, null);
            examiningPersonsWindow.ShowDialog();
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                Signatory = examiningPersonsWindow.viewModel.SelectedEmployee;
                InitializeSignatoryData();
            }
        }

        private void InitializeSignatoryData()
        {
            if (Signatory != null)
            {
                SignatoryDepartment = Signatory.Department.DepartmentTitle;
                SignatoryPosition = Signatory.Position;
                SignatoryFullName = Signatory.EmployeeFullName;
            }
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
            InternalDocumentFilesList.Add(internalDocumentFile);
            InitializeInternalDocumentFiles();
        }

        public ICommand IDeleteInternalDocumentFile => new RelayCommand(deleteInternalDocumentFile => DeleteInternalDocumentFile());
        private void DeleteInternalDocumentFile()
        {
            bool result = false;
            if (InternalDocument != null)
            {
                if (SelectedInternalDocumentFile != null)
                {
                    InternalDocumentFileService internalDocumentFileService = new InternalDocumentFileService();
                    result = internalDocumentFileService.RemoveInternalDocumentFile(SelectedInternalDocumentFile.InternalDocumentFileID).Result;
                }
                if (result)
                {
                    MessageBox.Show("Удаление файла выполнено");
                    GetInternalDocumentFiles();
                    InitializeInternalDocumentFiles();
                    return;
                }
                else
                {
                    MessageBox.Show("Ошибка. Удаление файла не выполнено!");
                }
            }
            else
            {
                InternalDocumentFilesList.Remove(SelectedInternalDocumentFile);
                InitializeInternalDocumentFiles();
                SelectedInternalDocumentFile = InternalDocumentFiles.FirstOrDefault();
            }
        }

        public ICommand IRegisterOrUpdateInternalDocument => new RelayCommand(registerOrUpdateInternalDocument => RegisterOrUpdateInternalDocument());
        private void RegisterOrUpdateInternalDocument()
        {
            if (!ValidationInternalDocument()) return;
            if (InternalDocument == null) RegisterInternalDocument();
            else UpdateInternalDocument();
        }
        
        private void RegisterInternalDocument()
        {
            InternalDocument newInternalDocument = CreateInternalDocument();
            bool result = false;
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            if (InternalDocument == null)
            {
                string type = InternalDocumentTypeConverter.ConvertToString(newInternalDocument.InternalDocumentType).Substring(0, 2);
                int number = internalDocumentsService.GetCountInternalDocumentByType(newInternalDocument.InternalDocumentType) + 1;
                newInternalDocument.InternalDocumentRegistrationNumber = $"{number}/{type}"; 
                newInternalDocument.RegistrationDate = DateTime.Now;
                newInternalDocument.IsRegistered = true;
            }
            if (internalDocumentsService.AddInternalDocument(newInternalDocument).Result)
            { 
                MessageBox.Show("Документ зарегистрирован");
                InternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в регистрации документа");
            }
        }

        private void UpdateInternalDocument()
        {
            InternalDocument.InternalDocumentType = SelectedInternalDocumentType;
            InternalDocument.InternalDocumentDate = InternalDocumentDate;
            InternalDocument.Signatory = Signatory;
            InternalDocument.SignatoryID = Signatory.EmployeeID;
            InternalDocument.ApprovedManager = ApprovedManager;
            if (InternalDocument.ApprovedManager != null)
            {
                InternalDocument.ApprovedManagerID = ApprovedManager.EmployeeID;
            }
            InternalDocument.EmployeeRecievedDocument = EmployeeRecievedDocument;
            if (EmployeeRecievedDocument != null)
            {
                InternalDocument.EmployeeRecievedDocumentID = EmployeeRecievedDocument.EmployeeID;
            }
            //InternalDocument.InternalDocumentFiles = InternalDocumentFiles.ToList();
            InternalDocument.InternalDocumentTitle = InternalDocumentTitle;
            InternalDocument.InternalDocumentContent = InternalDocumentContent;
            InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
            if (internalDocumentsService.UpdateInternalDocument(InternalDocument).Result)
            {
                MessageBox.Show("Изменения сохранены");
                InternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при внесении изменений");
            }
        }

        private InternalDocument CreateInternalDocument()
        {
            InternalDocument newInternalDocument = new InternalDocument();
            newInternalDocument.InternalDocumentType = SelectedInternalDocumentType;
            newInternalDocument.InternalDocumentDate = InternalDocumentDate;
            newInternalDocument.SignatoryID = Signatory.EmployeeID;
            newInternalDocument.Signatory = Signatory;
            newInternalDocument.InternalDocumentTitle = InternalDocumentTitle;
            newInternalDocument.InternalDocumentContent = InternalDocumentContent;
            if (ApprovedManager != null)
            {
                newInternalDocument.ApprovedManagerID = ApprovedManager.EmployeeID;
                newInternalDocument.ApprovedManager = ApprovedManager;
            }
            newInternalDocument.InternalDocumentFiles = InternalDocumentFiles.ToList();
            newInternalDocument.InternalDocumentStatus = DocumentStatus.UnderConsideration;
            newInternalDocument.InternalDocumentDate = InternalDocumentDate;
            if (InternalDocument != null)
            {
                newInternalDocument.InternalDocumentRegistrationNumber = InternalDocument.InternalDocumentRegistrationNumber;
                newInternalDocument.InternalDocumentID = InternalDocument.InternalDocumentID;
                newInternalDocument.RegistrationDate = InternalDocument.RegistrationDate;
                newInternalDocument.IsRegistered = InternalDocument.IsRegistered;
            }
            return newInternalDocument;
        }

        public ICommand IRemoveInternalDocument => new RelayCommand(removeInternalDocument => RemoveInternalDocument());
        private void RemoveInternalDocument()
        {
            SelectedInternalDocumentTypeIndex = 0;
            InternalDocumentDate = DateTime.Now;
            ApprovedManager = null;
            Signatory = null;
            InternalDocumentFilesList.Clear();
            InternalDocumentFiles.Clear();
            if (InternalDocument != null)
            {
                InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
                bool result = internalDocumentsService.RemoveInternalDocument(InternalDocument.InternalDocumentID).Result;
                if (result) MessageBox.Show($"Внутренний документ {InternalDocument.InternalDocumentTitle} успешно удален");
                else MessageBox.Show($"Ошибка при удалении внутреннего документа {InternalDocument.InternalDocumentTitle}");
                InternalDocumentWindow.Close();
            }
        }

        public ICommand ISendToExaminingPerson => new RelayCommand(sendToExaminingPerson => SendToExaminingPerson());
        private void SendToExaminingPerson()
        {
            if (!ValidationInternalDocument()) return;
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true, null);
            examiningPersonsWindow.ShowDialog();
            bool result = false;
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                InternalDocument internalDocument = CreateInternalDocument();
                internalDocument.EmployeeRecievedDocumentID = examiningPersonsWindow.viewModel.SelectedEmployee.EmployeeID;
                internalDocument.EmployeeRecievedDocument = examiningPersonsWindow.viewModel.SelectedEmployee;
                EmployeeRecievedDocument = internalDocument.EmployeeRecievedDocument;
                InternalDocumentsService internalDocumentsService = new InternalDocumentsService();
                if (internalDocument.IsRegistered == false)
                {
                    string type = InternalDocumentTypeConverter.ConvertToString(internalDocument.InternalDocumentType).Substring(0, 2);
                    int number = internalDocumentsService.GetCountInternalDocumentByType(internalDocument.InternalDocumentType) + 1;
                    internalDocument.InternalDocumentRegistrationNumber = $"{number}/{type}";
                    internalDocument.RegistrationDate = DateTime.Now;
                    internalDocument.InternalDocumentSendingDate = DateTime.Now;
                    internalDocument.IsRegistered = true;
                    result = internalDocumentsService.AddInternalDocument(internalDocument).Result;
                }
                else
                {
                    internalDocument.InternalDocumentSendingDate = DateTime.Now;
                    result = internalDocumentsService.UpdateInternalDocument(internalDocument).Result;
                }
            }
            if (result)
            {
                MessageBox.Show("Документ успешно направлен");
                InternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в отправке документа");
            }
        }

        private bool ValidationInternalDocument()
        {
            if (Signatory == null)
            {
                MessageBox.Show("Выберите подписанта внутреннего документа");
                InternalDocumentWindow.SignatoryInfo.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.SignatoryInfo.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                InternalDocumentWindow.SignatoryInfo.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.SignatoryInfo.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (InternalDocumentDate > DateTime.Now)
            {
                MessageBox.Show("Дата документа позже текущей");
                InternalDocumentWindow.InternalDocumentDate.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentDate.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                InternalDocumentWindow.InternalDocumentDate.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentDate.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (string.IsNullOrEmpty(InternalDocumentTitle))
            {
                MessageBox.Show("Не введено наименование документа");
                InternalDocumentWindow.InternalDocumentTitle.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                InternalDocumentWindow.InternalDocumentTitle.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentTitle.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (string.IsNullOrEmpty(InternalDocumentContent))
            {
                MessageBox.Show("Не введено краткое содержание документа");
                InternalDocumentWindow.InternalDocumentContent.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentContent.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                InternalDocumentWindow.InternalDocumentContent.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentContent.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            return true;
        }

        public ICommand IExit => new RelayCommand(exit => InternalDocumentWindow.Close());
    }
}

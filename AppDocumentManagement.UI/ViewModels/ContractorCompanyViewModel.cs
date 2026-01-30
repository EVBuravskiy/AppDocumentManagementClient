using AppDocumentManagement.UI.Utilities;
using AppDocumentManagement.UI.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AppDocumentManagement.Models;
using AppDocumentManagement.ExternalDocumentService.Services;

namespace AppDocumentManagement.UI.ViewModels
{
    /// <summary>
    /// ViewModel for ContractorCompanyWindow
    /// </summary>
    public class ContractorCompanyViewModel : BaseViewModelClass
    {
        /// <summary>
        /// ContractorCompanyWindow announcement
        /// </summary>
        private ContractorCompanyWindow ContractorCompanyWindow;
        /// <summary>
        /// Declaring a property for the ContractorCompany
        /// </summary>
        private ContractorCompany? ContractorCompany;
        /// <summary>
        /// Declaring a string for a company name
        /// </summary>
        private string contractorCompanyTitle;
        /// <summary>
        /// Declaring a property for the string representation of the company name
        /// </summary>
        public string ContractorCompanyTitle
        {
            get => contractorCompanyTitle;
            set
            {
                contractorCompanyTitle = value;
                OnPropertyChanged(nameof(ContractorCompanyTitle));
            }
        }
        /// <summary>
        /// Declaring a string for a short company name
        /// </summary>
        private string contractorCompanyShortTitle;
        /// <summary>
        /// Declaring a property for the short string representation of the company name
        /// </summary>
        public string ContractorCompanyShortTitle
        {
            get => contractorCompanyShortTitle;
            set
            {
                contractorCompanyShortTitle = value;
                OnPropertyChanged(nameof(ContractorCompanyShortTitle));
            }
        }
        /// <summary>
        /// Declaring a string for a company address
        /// </summary>
        private string contractorCompanyAddress;
        /// <summary>
        /// Declaring a property for the string representation of the company's address
        /// </summary>
        public string ContractorCompanyAddress
        {
            get => contractorCompanyAddress;
            set
            {
                contractorCompanyAddress = value;
                OnPropertyChanged(nameof(ContractorCompanyAddress));
            }
        }
        /// <summary>
        /// Declaring a string for a company phone
        /// </summary>
        private string contractorCompanyPhone;
        /// <summary>
        /// Declaring a property for the string representation of the company's phone
        /// </summary>
        public string ContractorCompanyPhone
        {
            get => contractorCompanyPhone;
            set
            {
                contractorCompanyPhone = value;
                OnPropertyChanged(nameof(ContractorCompanyPhone));
            }
        }
        /// <summary>
        /// Declaring a string for a company email
        /// </summary>
        private string contractorCompanyEmail;
        /// <summary>
        /// Declaring a property for the string representation of the company's email
        /// </summary>
        public string ContractorCompanyEmail
        {
            get => contractorCompanyEmail;
            set
            {
                contractorCompanyEmail = value;
                OnPropertyChanged(nameof(ContractorCompanyEmail));
            }
        }
        /// <summary>
        /// Declaring a string for information about a contractor company
        /// </summary>
        private string contractorCompanyInformation;
        /// <summary>
        /// Declaring a property for the string representation of the information about a contractor company
        /// </summary>
        public string ContractorCompanyInformation
        {
            get => contractorCompanyInformation;
            set
            {
                contractorCompanyInformation = value;
                OnPropertyChanged(nameof(ContractorCompanyInformation));
            }
        }
        /// <summary>
        /// Declaring a boolean value to define a new contractor company
        /// </summary>
        private bool isNew = true;
        /// <summary>
        /// ContractorCompanyViewModel constructor
        /// </summary>
        /// <param name="contractorCompanyWindow"></param>
        /// <param name="selectedContractorCompany"></param>
        public ContractorCompanyViewModel(ContractorCompanyWindow contractorCompanyWindow, ContractorCompany selectedContractorCompany)
        {
            ContractorCompanyWindow = contractorCompanyWindow;
            if (selectedContractorCompany != null)
            {
                ContractorCompany = selectedContractorCompany;
                isNew = false;
                ContractorCompanyTitle = selectedContractorCompany.ContractorCompanyTitle;
                ContractorCompanyShortTitle = selectedContractorCompany.ContractorCompanyShortTitle;
                ContractorCompanyAddress = selectedContractorCompany.ContractorCompanyAddress;
                ContractorCompanyPhone = selectedContractorCompany.ContractorCompanyPhone;
                ContractorCompanyEmail = selectedContractorCompany.ContractorCompanyEmail;
                ContractorCompanyInformation = selectedContractorCompany.ContractorCompanyInformation;
            }
        }
        /// <summary>
        /// Declaration of the command to save the contractor company
        /// </summary>
        public ICommand ISaveContractorCompany => new RelayCommand(saveContractorCompany => SaveContractorCompany());
        private void SaveContractorCompany()
        {
            if (!ValidateContractorCompany()) return;
            if (isNew)
            {
                ContractorCompany = new ContractorCompany();
            }
            ContractorCompany.ContractorCompanyTitle = ContractorCompanyTitle;
            ContractorCompany.ContractorCompanyShortTitle = ContractorCompanyShortTitle;
            ContractorCompany.ContractorCompanyAddress = ContractorCompanyAddress;
            ContractorCompany.ContractorCompanyPhone = ContractorCompanyPhone;
            ContractorCompany.ContractorCompanyEmail = ContractorCompanyEmail;
            ContractorCompany.ContractorCompanyInformation = ContractorCompanyInformation;
            ContractorCompanyService contractorCompanyService = new ContractorCompanyService();
            bool result = false;
            if (isNew)
            {
                result = contractorCompanyService.AddContractorCompany(ContractorCompany).Result;
            }
            else
            {
                result = contractorCompanyService.UpdateContractorCompany(ContractorCompany).Result;
            }
            if (result)
            {
                MessageBox.Show($"Контрагент {ContractorCompany.ContractorCompanyTitle} сохранен");
            }
            else
            {
                MessageBox.Show($"Ошибка! Контрагент {ContractorCompany.ContractorCompanyTitle} не сохранен");
            }
            Exit();
        }
        /// <summary>
        /// Function for checking entered data for a contractor company
        /// </summary>
        /// <returns></returns>
        private bool ValidateContractorCompany()
        {
            if (string.IsNullOrEmpty(ContractorCompanyTitle))
            {
                MessageBox.Show("Введите наименование организации");
                ContractorCompanyWindow.ContractorCompanyTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(ContractorCompanyShortTitle))
            {
                MessageBox.Show("Введите сокращенное наименование организации");
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyShortTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(ContractorCompanyAddress))
            {
                MessageBox.Show("Введите адрес организации");
                ContractorCompanyWindow.ContractorCompanyAddress.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyAddress.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyAddress.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyAddress.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(ContractorCompanyEmail))
            {
                MessageBox.Show("Не введен адрес email организации");
                ContractorCompanyWindow.ContractorCompanyEmail.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else if(!ValidateData.ValidateEmail(ContractorCompanyEmail, ContractorCompanyEmail.Length))
            {
                MessageBox.Show("Неправильно введен email организации");
                ContractorCompanyWindow.ContractorCompanyEmail.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyEmail.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyEmail.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(ContractorCompanyPhone))
            {
                MessageBox.Show("Не введен номер телефона организации");
                ContractorCompanyWindow.ContractorCompanyPhone.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyPhone.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else if(!ValidateData.ValidatePhone(ContractorCompanyPhone, ContractorCompanyPhone.Length))
            {
                MessageBox.Show("Неправильно введен номер телефона организации");
                ContractorCompanyWindow.ContractorCompanyPhone.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyPhone.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                ContractorCompanyWindow.ContractorCompanyPhone.BorderThickness = new System.Windows.Thickness(2);
                ContractorCompanyWindow.ContractorCompanyPhone.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            return true;
        }
        /// <summary>
        /// Declaration of the command to remove the selected contractor company
        /// </summary>
        public ICommand IRemoveContractorCompany => new RelayCommand(removeContractorCompany => RemoveContractorCompany());
        /// <summary>
        /// Function to remove the selected contractor company
        /// </summary>
        private void RemoveContractorCompany()
        {
            bool result = false;
            if (ContractorCompany != null)
            {
                ContractorCompanyService contractorCompanyService = new ContractorCompanyService();
                result = contractorCompanyService.RemoveContractorCompany(ContractorCompany.ContractorCompanyID).Result;
                if (result)
                {
                    MessageBox.Show($"Контрагент {ContractorCompany.ContractorCompanyTitle} удален");
                }
                else
                {
                    MessageBox.Show($"Ошибка! Контрагент {ContractorCompany.ContractorCompanyTitle} не удален");
                }
                Exit();
            }
            if (!result)
            {
                MessageBox.Show($"Ошибка! Контрагент не был получен");
            }
            Exit();
        }
        /// <summary>
        /// Declaration of the command to close ContractorCompanyWindow
        /// </summary>
        public ICommand IExit => new RelayCommand(exit => Exit());
        /// <summary>
        /// Function to close ContractorCompanyWindow
        /// </summary>
        private void Exit()
        {
            ContractorCompanyWindow.Close();
        }
    }
}

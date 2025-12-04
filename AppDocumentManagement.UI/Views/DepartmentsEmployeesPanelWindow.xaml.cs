using AppDocumentManagement.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsEmployeesPanelWindow.xaml
    /// </summary>
    public partial class DepartmentsEmployeesPanelWindow : Window
    {
        private DepartmentsEmployeesPanelViewModel _departmentsEmployeesPanelViewModel;

        public DepartmentsEmployeesPanelWindow(int currentUserID)
        {
            InitializeComponent();
            _departmentsEmployeesPanelViewModel = new DepartmentsEmployeesPanelViewModel(this, currentUserID);
            DataContext = _departmentsEmployeesPanelViewModel;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _departmentsEmployeesPanelViewModel.SearchString = textBox.Text;
            _departmentsEmployeesPanelViewModel.FindItems(textBox.Text);
        }
    }
}

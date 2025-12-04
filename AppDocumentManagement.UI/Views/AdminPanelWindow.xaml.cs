using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        private AdminPanelViewModel _adminPanelViewModel;

        public AdminPanelWindow(int currentUserID)
        {
            InitializeComponent();
            _adminPanelViewModel = new AdminPanelViewModel(this, currentUserID);
            DataContext = _adminPanelViewModel;
        }

    }
}

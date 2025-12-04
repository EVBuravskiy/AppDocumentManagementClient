using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        private Department _selecedDepartment = null;
        private DepartmentViewModel _viewModel;

        public DepartmentWindow(Department inputDepartment)
        {
            _selecedDepartment = inputDepartment;
            InitializeComponent();
            _viewModel = new DepartmentViewModel(this, inputDepartment);
            DataContext = _viewModel;
        }
    }
}

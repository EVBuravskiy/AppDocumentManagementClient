using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;


namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductionTaskShowWindow.xaml
    /// </summary>
    public partial class ProductionTaskShowWindow : Window
    {
        private ProductionTaskShowViewModel viewModel;
        public ProductionTaskShowWindow(Employee currentEmployee, ProductionTask currentProductionTask)
        {
            InitializeComponent();
            viewModel = new ProductionTaskShowViewModel(this, currentEmployee, currentProductionTask);
            DataContext = viewModel;
        }
    }
}

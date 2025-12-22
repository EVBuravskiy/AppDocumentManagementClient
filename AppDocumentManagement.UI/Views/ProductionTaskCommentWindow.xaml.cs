using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;


namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductionTaskCommentWindow.xaml
    /// </summary>
    public partial class ProductionTaskCommentWindow : Window
    {
        public ProductionTaskCommentViewModel viewModel { get; set; }
        public ProductionTaskCommentWindow(ProductionTask currentProductionTask, Employee currentEmployee)
        {
            viewModel = new ProductionTaskCommentViewModel(this, currentProductionTask, currentEmployee);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

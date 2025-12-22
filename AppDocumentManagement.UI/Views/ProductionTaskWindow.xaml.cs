using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductionTaskWindow.xaml
    /// </summary>
    public partial class ProductionTaskWindow : Window
    {
        private ProductionTaskViewModel viewModel;

        public ProductionTaskWindow(Employee currentEmployee, ExternalDocument externalDocument, InternalDocument internalDocument)
        {
            InitializeComponent();
            viewModel = new ProductionTaskViewModel(this, currentEmployee, externalDocument, internalDocument);
            DataContext = viewModel;
        }
    }
}

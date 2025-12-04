using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        private DocumentViewModel viewController;
        public DocumentWindow(ExternalDocument inputDocument)
        {
            InitializeComponent();
            viewController = new DocumentViewModel(this, inputDocument);
            DataContext = viewController;
        }
    }
}

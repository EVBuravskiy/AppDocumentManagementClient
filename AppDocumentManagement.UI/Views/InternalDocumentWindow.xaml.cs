using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;


namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для InternalDocumentWindow.xaml
    /// </summary>
    public partial class InternalDocumentWindow : Window
    {
        private InternalDocumentViewModel viewModel;
        public InternalDocumentWindow(InternalDocument inputDocument)
        {
            InitializeComponent();
            viewModel = new InternalDocumentViewModel(this, inputDocument);
            DataContext = viewModel;
        }
    }
}

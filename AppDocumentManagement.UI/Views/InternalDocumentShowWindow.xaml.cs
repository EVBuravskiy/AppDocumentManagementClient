using AppDocumentManagement.Models;
using AppDocumentManagement.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagement.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для InternalDocumentShowWindow.xaml
    /// </summary>
    public partial class InternalDocumentShowWindow : Window
    {
        private InternalDocumentShowViewModel viewModel;

        public InternalDocumentShowWindow(InternalDocument internalDocument, int currentEmployeeID)
        {
            InitializeComponent();
            viewModel = new InternalDocumentShowViewModel(this, internalDocument, currentEmployeeID);
            DataContext = viewModel;
        }
    }
}

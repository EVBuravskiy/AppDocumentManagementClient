namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// File dialog service interface
    /// </summary>
    public interface IFileDialogService
    {
        string OpenFile();
        string OpenFile(string fileExtension);
        void ShowMessageBox(string message);
        string SaveFile(string fileExtension, string fileName);
    }
}

using Microsoft.Win32;
using System.Data;
using System.IO;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Window dialog class
    /// </summary>
    public class WindowsDialogService : IFileDialogService
    {
        /// <summary>
        /// Function to open file
        /// </summary>
        /// <returns>file name</returns>
        public string OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf|Image files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }
        /// <summary>
        /// Function to open file by file extension
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns>file name</returns>
        public string OpenFile(string fileExtension)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = fileExtension;
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }
        /// <summary>
        /// Function to save file
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="fileName"></param>
        /// <returns>file name</returns>
        public string SaveFile(string fileExtension, string fileName)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = GetFileFilter(fileExtension);
            dialog.FileName = fileName;
            dialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                int index = dialog.FileName.LastIndexOf(".");
                string fileWithoutExtension = dialog.FileName.Substring(0, index);
                string filePath = $"{fileWithoutExtension}.{fileExtension}";
                return filePath;
            }
            return null;
        }
        /// <summary>
        /// Function to show message box with input message
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ShowMessageBox(string message)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Function to get a file by filter
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns>string</returns>
        private string GetFileFilter(string fileExtension)
        {
            return fileExtension switch
            {
                "txt" => "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                "pdf" => "Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*",
                "BMP" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "bmp" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "JPG" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "jpg" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "GIF" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "gif" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "PNG" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                "png" => "Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
                _ => "Text files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf|Image files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*",
            };
        }
    }
}

using System.Diagnostics;
using System.IO;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Directory processing class
    /// </summary>
    public class DirectoryProcessing
    {
        /// <summary>
        /// Function to open directory
        /// </summary>
        /// <param name="folderPath"></param>
        public static void OpenDirectory(string folderPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = folderPath
            };
            Process.Start(psi);
        }
    }
}

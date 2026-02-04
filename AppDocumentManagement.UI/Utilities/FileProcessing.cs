using AppDocumentManagement.Models;
using System.IO;
using System.Windows;


namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// File processing class
    /// </summary>
    public class FileProcessing
    {
        /// <summary>
        /// Function to get file name
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string</returns>
        public static string GetFileName(string filePath)
        {
            int lastIndex = filePath.LastIndexOf('\\');
            return filePath.Substring(lastIndex + 1);
        }
        /// <summary>
        /// Function to get file extension
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string</returns>
        public static string GetFileExtension(string filePath)
        {
            int lastIndex = filePath.LastIndexOf(".");
            return filePath.Substring(lastIndex + 1);
        }
        /// <summary>
        /// Function to convert file data into a byte array
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>byte[]</returns>
        public static byte[] GetFileData(string filePath)
        {
            byte[] buffer = new byte[0];
            if (!File.Exists(filePath))
            {
                return buffer;
            }
            try //ошибка в изменении ранее открытого файла
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
            return buffer;
        }
        /// <summary>
        /// Function to save ExternalDocumentFile to folder
        /// </summary>
        /// <param name="documentFile"></param>
        /// <param name="filePath"></param>
        /// <returns>bool</returns>
        public static bool SaveDocumentToFolder(ExternalDocumentFile documentFile, string filePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(filePath))
            {
                string directoryPath = $"{Directory.GetCurrentDirectory}\\ExternalDocuments\\";
                filePath = $"{directoryPath}{documentFile.ExternalFileName}";
                //ошибка в наличии файла при его сохранении в папку?
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n");
                        Console.WriteLine("Ошибка в удалении изображения");
                    }
                }
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fileStream.Write(documentFile.ExternalFileData, 0, documentFile.ExternalFileData.Length);
                fileStream.Close(); //ошибка в закрытии файла?
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Function to save InternalDocumentFile to folder
        /// </summary>
        /// <param name="internalDocumentFile"></param>
        /// <param name="filePath"></param>
        /// <returns>bool</returns>
        public static bool SaveInternalDocumentToFolder(InternalDocumentFile internalDocumentFile, string filePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(filePath))
            {
                string directoryPath = $"{Directory.GetCurrentDirectory}\\InternalDocuments\\";
                filePath = $"{directoryPath}{internalDocumentFile.FileName}";
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fileStream.Write(internalDocumentFile.FileData, 0, internalDocumentFile.FileData.Length);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Function for saving an EmployeePhoto to temporary folder
        /// </summary>
        /// <param name="photo"></param>
        /// <returns>string</returns>
        public static string SaveEmployeePhotoToTempFolder(EmployeePhoto photo)
        {
            if (photo == null)
            {
                MessageBox.Show("Ошибка! Не удалось сохранить файл");
                return null;
            }
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\TempEmployeePhotos\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{photo.FileName}";
            if(File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                try
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        fileStream.Write(photo.FileData, 0, photo.FileData.Length);
                    }
                    return filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Не удалось сохранить файл");
                    return null;
                }
            }
        }
        /// <summary>
        /// Function for copying a file to a temporary folder
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns>string</returns>
        public static string CopyFileToTempFolder(string sourcePath)
        {
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\TempEmployeePhotos\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = GetFileName(sourcePath);
            string destPath = $"{directoryPath}{fileName}";
            if (File.Exists(sourcePath) && !File.Exists(destPath))
            {
                File.Copy(sourcePath, destPath);
            }
            return destPath;
        }
        /// <summary>
        /// Function to check file exist
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>bool</returns>
        public static bool CheckFileExist(string fileName)
        {
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\TempEmployeePhotos\\";
            string sourcePath = $"{directoryPath}{fileName}";
            if (!Directory.Exists(directoryPath))
            {
                if (File.Exists(sourcePath))
                {
                    return true;
                }
            }
            else if (File.Exists(sourcePath))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Function for saving a file from a database
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="directoryName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public static bool SaveFileFromDB(string fileName, string directoryName, string fileExtension, byte[] fileData)
        {
            bool result = false;
            if (string.IsNullOrEmpty(fileName)) return false;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{fileName}";
            if (!File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    fileStream.Write(fileData, 0, fileData.Length);
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Function for saving an InternalDocumentFile from a database
        /// </summary>
        /// <param name="internalDocumentFile"></param>
        /// <param name="directoryName"></param>
        /// <returns>string</returns>
        public static string SaveInternalDocumentFileFromDB(InternalDocumentFile internalDocumentFile, string directoryName)
        {
            if (internalDocumentFile == null) return string.Empty;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{internalDocumentFile.FileName}";
            SaveInternalDocumentFileToPath(filePath, internalDocumentFile);
            return directoryPath;
        }
        /// <summary>
        /// Function to save an InternalDocumentFile to the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="internalDocumentFile"></param>
        /// <returns>bool</returns>
        public static bool SaveInternalDocumentFileToPath(string path, InternalDocumentFile internalDocumentFile)
        {
            bool result = false;
            if (string.IsNullOrEmpty(path)) return result;
            if (!File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    fileStream.Write(internalDocumentFile.FileData, 0, internalDocumentFile.FileData.Length);
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Function for saving an ExternalDocumentFile from a database
        /// </summary>
        /// <param name="externalDocumentFile"></param>
        /// <param name="directoryName"></param>
        /// <returns>string</returns>
        public static string SaveExternalDocumentFileFromDB(ExternalDocumentFile externalDocumentFile, string directoryName)
        {
            if (externalDocumentFile == null) return string.Empty;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{externalDocumentFile.ExternalFileName}";
            SaveExternalDocumentFileToPath(filePath, externalDocumentFile);
            return directoryPath;
        }
        /// <summary>
        /// Function to save an ExternalDocumentFile to the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="externalDocumentFile"></param>
        /// <returns>bool</returns>
        public static bool SaveExternalDocumentFileToPath(string path, ExternalDocumentFile externalDocumentFile)
        {
            bool result = false;
            if (string.IsNullOrEmpty(path)) return result;
            if (!File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    fileStream.Write(externalDocumentFile.ExternalFileData, 0, externalDocumentFile.ExternalFileData.Length);
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Function for saving a ProductionTaskFile from a database
        /// </summary>
        /// <param name="productionTaskFile"></param>
        /// <param name="directoryName"></param>
        /// <returns>string</returns>
        public static string SaveProductionTaskFileFromDB(ProductionTaskFile productionTaskFile, string directoryName)
        {
            if (productionTaskFile == null) return string.Empty;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{productionTaskFile.ProductionTaskFileName}";
            SaveProductionTaskFileToPath(filePath, productionTaskFile);
            return directoryPath;
        }
        /// <summary>
        /// Function to save a ProductionTaskFile to the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="productionTaskFile"></param>
        /// <returns>bool</returns>
        public static bool SaveProductionTaskFileToPath(string path, ProductionTaskFile productionTaskFile)
        {
            bool result = false;
            if (string.IsNullOrEmpty(path)) return result;
            if (!File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    fileStream.Write(productionTaskFile.ProductionTaskFileData, 0, productionTaskFile.ProductionTaskFileData.Length);
                    result = true;
                }
            }
            return result;
        }
    }
}

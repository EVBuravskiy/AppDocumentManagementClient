using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.EmployeesService.Converters
{
    /// <summary>
    /// MEmployeePhoto Message Converter Class
    /// </summary>
    public class MEmployeePhotoConverter
    {
        /// <summary>
        /// Function to convert from MEmployeePhoto message to EmployeePhoto class
        /// </summary>
        /// <param name="mEmployeePhoto"></param>
        /// <returns>EmployeePhoto</returns>
        public static EmployeePhoto ConvertToEmployeePhoto(MEmployeePhoto mEmployeePhoto)
        {
            EmployeePhoto employeePhoto = new EmployeePhoto();
            employeePhoto.EmployeePhotoID = mEmployeePhoto.EmployeePhotoID;
            employeePhoto.FileName = mEmployeePhoto.FileName;
            employeePhoto.FileExtension = mEmployeePhoto.FileExtension;
            employeePhoto.FileData = mEmployeePhoto.FileData.ToByteArray();
            employeePhoto.EmployeeID = mEmployeePhoto.EmployeeID;
            return employeePhoto;
        }

        /// <summary>
        /// Function to convert from EmployeePhoto class to MEmployeePhoto message
        /// </summary>
        /// <param name="mEmployeePhoto"></param>
        /// <returns>MEmployeePhoto</returns>
        public static MEmployeePhoto ConvertToMEmployeePhoto(EmployeePhoto employeePhoto)
        {
            MEmployeePhoto mEmployeePhoto = new MEmployeePhoto();
            mEmployeePhoto.EmployeePhotoID = employeePhoto.EmployeePhotoID;
            mEmployeePhoto.FileName = employeePhoto.FileName;
            mEmployeePhoto.FileExtension = employeePhoto.FileExtension;
            mEmployeePhoto.FileData = ByteString.CopyFrom(employeePhoto.FileData);
            mEmployeePhoto.EmployeeID = employeePhoto.EmployeeID;
            return mEmployeePhoto;
        }
    }
}

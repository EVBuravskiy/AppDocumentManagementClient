using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.EmployeeService.Converters
{
    public class MEmployeePhotoConverter
    {
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

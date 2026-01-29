using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    /// <summary>
    /// MExternalDocumentFile Message Converter Class
    /// </summary>
    public class MExternalDocumentFileConverter
    {
        /// <summary>
        /// Function to convert from MExternalDocumentFile message to ExternalDocumentFile class
        /// </summary>
        /// <param name="mExternalDocumentFile"></param>
        /// <returns>ExternalDocumentFile</returns>
        public static ExternalDocumentFile ConvertToExternalDocumentFile(MExternalDocumentFile mExternalDocumentFile)
        {
            ExternalDocumentFile externalDocumentFile= new ExternalDocumentFile();
            externalDocumentFile.ExternalDocumentFileID = mExternalDocumentFile.ExternalDocumentFileID;
            externalDocumentFile.ExternalFileName = mExternalDocumentFile.ExternalDocumentFileName;
            externalDocumentFile.ExternalFileExtension = mExternalDocumentFile.ExternalDocumentFileExternsion;
            externalDocumentFile.ExternalFileData = mExternalDocumentFile.FileData.ToByteArray();
            if (mExternalDocumentFile.ExternalDocumentID != 0)
            {
                externalDocumentFile.ExternalDocumentID = mExternalDocumentFile.ExternalDocumentID;
            }
            return externalDocumentFile;
        }
        /// <summary>
        /// Function to convert from ExternalDocumentFile class to MExternalDocumentFile message
        /// </summary>
        /// <param name="externalDocumentFile"></param>
        /// <returns>MExternalDocumentFile</returns>
        public static MExternalDocumentFile ConvertToMExternalDocumentFile(ExternalDocumentFile externalDocumentFile)
        {
            MExternalDocumentFile mExternalDocumentFile = new MExternalDocumentFile();
            mExternalDocumentFile.ExternalDocumentFileID = externalDocumentFile.ExternalDocumentFileID;
            mExternalDocumentFile.ExternalDocumentFileName = externalDocumentFile.ExternalFileName;
            mExternalDocumentFile.ExternalDocumentFileExternsion = externalDocumentFile.ExternalFileExtension;
            mExternalDocumentFile.FileData = ByteString.CopyFrom(externalDocumentFile.ExternalFileData);
            if (externalDocumentFile.ExternalDocument != null)
            {
                mExternalDocumentFile.ExternalDocumentID = externalDocumentFile.ExternalDocument.ExternalDocumentID;
            }
            else if (externalDocumentFile.ExternalDocumentID != 0)
            {
                mExternalDocumentFile.ExternalDocumentID = externalDocumentFile.ExternalDocumentID;
            }
            return mExternalDocumentFile;
        }
    }
}

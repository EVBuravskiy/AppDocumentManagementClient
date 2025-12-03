using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    public class MExternalDocumentFileConverter
    {
        public static ExternalDocumentFile ConvertToExternalDocumentFile(MExternalDocumentFile mExternalDocumentFile)
        {
            ExternalDocumentFile externalDocumentFile= new ExternalDocumentFile();
            externalDocumentFile.ExternalDocumentFileID = mExternalDocumentFile.ExternalDocumentFileID;
            externalDocumentFile.ExternalFileName = mExternalDocumentFile.ExternalDocumentFileName;
            externalDocumentFile.ExternalFileExtension = mExternalDocumentFile.ExternalDocumentFileExternsion;
            externalDocumentFile.ExternalFileData = mExternalDocumentFile.FileData.ToByteArray();
            externalDocumentFile.ExternalDocumentID = mExternalDocumentFile.ExternalDocumentID;
            return externalDocumentFile;
        }

        public static MExternalDocumentFile ConvertToMExternalDocumentFile(ExternalDocumentFile externalDocumentFile)
        {
            MExternalDocumentFile mExternalDocumentFile = new MExternalDocumentFile();
            mExternalDocumentFile.ExternalDocumentFileID = externalDocumentFile.ExternalDocumentFileID;
            mExternalDocumentFile.ExternalDocumentFileName = externalDocumentFile.ExternalFileName;
            mExternalDocumentFile.ExternalDocumentFileExternsion = externalDocumentFile.ExternalFileExtension;
            mExternalDocumentFile.FileData = ByteString.CopyFrom(externalDocumentFile.ExternalFileData);
            mExternalDocumentFile.ExternalDocumentID = externalDocumentFile.ExternalDocumentID;
            return mExternalDocumentFile;
        }
    }
}

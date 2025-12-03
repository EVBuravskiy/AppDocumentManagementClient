using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    public class MInternalDocumentFileConverter
    {
        public static InternalDocumentFile ConvertToInternalDocumentFile(MInternalDocumentFile mInternalDocumentFile)
        {
            InternalDocumentFile internalDocumentFile = new InternalDocumentFile();
            internalDocumentFile.InternalDocumentFileID = mInternalDocumentFile.InternalDocumentID;
            internalDocumentFile.FileName = mInternalDocumentFile.FileName;
            internalDocumentFile.FileExtension = mInternalDocumentFile.FileExtension;
            internalDocumentFile.FileData = mInternalDocumentFile.FileData.ToByteArray();
            internalDocumentFile.InternalDocumentID = mInternalDocumentFile.InternalDocumentID;
            return internalDocumentFile;
        }

        public static MInternalDocumentFile ConvertToMInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            MInternalDocumentFile mInternalDocumentFile = new MInternalDocumentFile();
            mInternalDocumentFile.InternalDocumentFileID = internalDocumentFile.InternalDocumentID;
            mInternalDocumentFile.FileName = internalDocumentFile.FileName;
            mInternalDocumentFile.FileExtension = internalDocumentFile.FileExtension;
            mInternalDocumentFile.FileData = ByteString.CopyFrom(internalDocumentFile.FileData);
            mInternalDocumentFile.InternalDocumentID = internalDocumentFile.InternalDocumentID;
            return mInternalDocumentFile;
        }
    }
}

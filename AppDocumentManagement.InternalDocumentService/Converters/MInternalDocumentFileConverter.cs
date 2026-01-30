using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    /// <summary>
    /// MInternalDocumentFile Message Converter Class
    /// </summary>
    public class MInternalDocumentFileConverter
    {
        /// <summary>
        /// Function to convert from MInternalDocumentFile message to InternalDocumentFile class
        /// </summary>
        /// <param name="mInternalDocumentFile"></param>
        /// <returns>InternalDocumentFile</returns>
        public static InternalDocumentFile ConvertToInternalDocumentFile(MInternalDocumentFile mInternalDocumentFile)
        {
            InternalDocumentFile internalDocumentFile = new InternalDocumentFile();
            internalDocumentFile.InternalDocumentFileID = mInternalDocumentFile.InternalDocumentFileID;
            internalDocumentFile.FileName = mInternalDocumentFile.FileName;
            internalDocumentFile.FileExtension = mInternalDocumentFile.FileExtension;
            internalDocumentFile.FileData = mInternalDocumentFile.FileData.ToByteArray();
            if (mInternalDocumentFile.InternalDocumentID != 0)
            {
                internalDocumentFile.InternalDocumentID = mInternalDocumentFile.InternalDocumentID;
            }
            return internalDocumentFile;
        }
        /// <summary>
        /// Function to convert from InternalDocumentFile class to MInternalDocumentFile message
        /// </summary>
        /// <param name="internalDocumentFile"></param>
        /// <returns>MInternalDocumentFile</returns>
        public static MInternalDocumentFile ConvertToMInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            MInternalDocumentFile mInternalDocumentFile = new MInternalDocumentFile();
            mInternalDocumentFile.InternalDocumentFileID = internalDocumentFile.InternalDocumentFileID;
            mInternalDocumentFile.FileName = internalDocumentFile.FileName;
            mInternalDocumentFile.FileExtension = internalDocumentFile.FileExtension;
            mInternalDocumentFile.FileData = ByteString.CopyFrom(internalDocumentFile.FileData);
            if (internalDocumentFile.InternalDocument != null)
            {
                mInternalDocumentFile.InternalDocumentID = internalDocumentFile.InternalDocument.InternalDocumentID;
            }
            else if (internalDocumentFile.InternalDocumentID != 0)
            {
                mInternalDocumentFile.InternalDocumentID = internalDocumentFile.InternalDocumentID;
            }
            return mInternalDocumentFile;
        }
    }
}

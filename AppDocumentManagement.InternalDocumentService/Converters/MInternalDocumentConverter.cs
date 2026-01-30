using AppDocumentManagement.Models;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    /// <summary>
    /// MInternalDocument Message Converter Class
    /// </summary>
    public class MInternalDocumentConverter
    {
        /// <summary>
        /// Function to convert from MInternalDocument message to InternalDocument class
        /// </summary>
        /// <param name="mInternalDocument"></param>
        /// <returns>InternalDocument</returns>
        public static InternalDocument ConvertToInternalDocument(MInternalDocument mInternalDocument)
        {
            InternalDocument internalDocument = new InternalDocument();
            internalDocument.InternalDocumentID = mInternalDocument.InternalDocumentID; //1
            internalDocument.InternalDocumentType = InternalDocumentTypeConverter.BackConvert(mInternalDocument.InternalDocumentType); //2
            if (mInternalDocument.InternalDocumentDate != "") //3
            {
                internalDocument.InternalDocumentDate = DateTime.Parse(mInternalDocument.InternalDocumentDate);
            }
            internalDocument.SignatoryID = mInternalDocument.InternalDocumentSygnatoryID; //4
            internalDocument.ApprovedManagerID = mInternalDocument.InternalDocumentApprovedManagerID; //5
            internalDocument.EmployeeRecievedDocumentID = mInternalDocument.InternalDocumentRecievedEmployeeID; //6
            if (mInternalDocument.RegistrationDate != "") //7
            {
                internalDocument.RegistrationDate = DateTime.Parse(mInternalDocument.RegistrationDate);
            }
            internalDocument.InternalDocumentRegistrationNumber = mInternalDocument.InternalDocumentRegistrationNumber ?? ""; //8
            internalDocument.IsRegistered = mInternalDocument.IsRegistered; //9
            if (mInternalDocument.InternalDocumentSendingDate != "") //10
            {
                internalDocument.InternalDocumentSendingDate = DateTime.Parse(mInternalDocument.InternalDocumentSendingDate);
            }
            internalDocument.InternalDocumentStatus = DocumentStatusConverter.BackConvert(mInternalDocument.InternalDocumentStatus); //11
            internalDocument.InternalDocumentTitle = mInternalDocument.InternalDocumentTitle; //12
            internalDocument.InternalDocumentContent = mInternalDocument.InternalDocumentContent; //13
            if (mInternalDocument.InternalDocumentFiles != null && mInternalDocument.InternalDocumentFiles.Count > 0) //14
            {
                internalDocument.InternalDocumentFiles = new List<InternalDocumentFile>();
                foreach (MInternalDocumentFile mfile in mInternalDocument.InternalDocumentFiles)
                {
                    InternalDocumentFile file = MInternalDocumentFileConverter.ConvertToInternalDocumentFile(mfile);
                    internalDocument.InternalDocumentFiles.Add(file);
                }
            }
            return internalDocument;
        }
        /// <summary>
        /// Function to convert from InternalDocument class to MInternalDocument message
        /// </summary>
        /// <param name="internalDocument"></param>
        /// <returns>MInternalDocument</returns>
        public static MInternalDocument ConvertToMInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = new MInternalDocument();
            mInternalDocument.InternalDocumentID = internalDocument.InternalDocumentID;
            mInternalDocument.InternalDocumentType = InternalDocumentTypeConverter.ToIntConvert(internalDocument.InternalDocumentType); 
            mInternalDocument.InternalDocumentDate = internalDocument.InternalDocumentDate.ToShortDateString();
            if (internalDocument.Signatory != null) 
            {
                mInternalDocument.InternalDocumentSygnatoryID = internalDocument.Signatory.EmployeeID;
            }
            else if (internalDocument.SignatoryID != 0)
            {
                mInternalDocument.InternalDocumentSygnatoryID = internalDocument.SignatoryID;
            }
            if (internalDocument.ApprovedManager != null) 
            {
                mInternalDocument.InternalDocumentApprovedManagerID = internalDocument.ApprovedManager.EmployeeID;
            }
            else if (internalDocument.ApprovedManagerID != 0)
            {
                mInternalDocument.InternalDocumentApprovedManagerID = internalDocument.ApprovedManagerID;
            }
            if (internalDocument.EmployeeRecievedDocument != null) 
            {
                mInternalDocument.InternalDocumentRecievedEmployeeID = internalDocument.EmployeeRecievedDocument.EmployeeID;
            }
            else if (internalDocument.EmployeeRecievedDocumentID != 0)
            {
                mInternalDocument.InternalDocumentRecievedEmployeeID = internalDocument.EmployeeRecievedDocumentID;
            }
            mInternalDocument.RegistrationDate = internalDocument.RegistrationDate.ToShortDateString();
            mInternalDocument.InternalDocumentRegistrationNumber = internalDocument.InternalDocumentRegistrationNumber ?? ""; //8
            mInternalDocument.IsRegistered = internalDocument.IsRegistered; 
            if (internalDocument.InternalDocumentSendingDate != null) 
            {
                mInternalDocument.InternalDocumentSendingDate = internalDocument.InternalDocumentSendingDate.ToString();
            }
            mInternalDocument.InternalDocumentStatus = DocumentStatusConverter.ToIntConvert(internalDocument.InternalDocumentStatus); //11
            mInternalDocument.InternalDocumentTitle = internalDocument.InternalDocumentTitle; //12
            mInternalDocument.InternalDocumentContent = internalDocument.InternalDocumentContent; //13
            if (internalDocument.InternalDocumentFiles != null && internalDocument.InternalDocumentFiles.Count > 0) //14
            {
                foreach (InternalDocumentFile file in internalDocument.InternalDocumentFiles)
                {
                    MInternalDocumentFile mInternalDocumentFile = MInternalDocumentFileConverter.ConvertToMInternalDocumentFile(file);
                    mInternalDocument.InternalDocumentFiles.Add(mInternalDocumentFile);
                }
            }
            return mInternalDocument;
        }
    }
}

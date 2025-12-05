using AppDocumentManagement.Models;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    public class MInternalDocumentConverter
    {

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
            internalDocument.InternalDocumentRegistrationNumber = mInternalDocument.InternalDocumentRegistrationNumber; //8
            internalDocument.IsRegistated = mInternalDocument.IsRegistated; //9
            if (mInternalDocument.SendingDate != "") //10
            {
                internalDocument.SendingDate = DateTime.Parse(mInternalDocument.SendingDate);
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

        public static MInternalDocument ConvertToMInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = new MInternalDocument();
            mInternalDocument.InternalDocumentID = internalDocument.InternalDocumentID; //1
            mInternalDocument.InternalDocumentType = InternalDocumentTypeConverter.ToIntConvert(internalDocument.InternalDocumentType); //2
            if (internalDocument.InternalDocumentDate != null) //3
            {
                mInternalDocument.InternalDocumentDate = internalDocument.InternalDocumentDate.ToShortDateString();
            }
            if (internalDocument.Signatory != null) //4
            {
                mInternalDocument.InternalDocumentSygnatoryID = internalDocument.Signatory.EmployeeID;
            }
            else if (internalDocument.SignatoryID != 0)
            {
                mInternalDocument.InternalDocumentSygnatoryID = internalDocument.SignatoryID;
            }
            if (internalDocument.ApprovedManager != null) //5
            {
                mInternalDocument.InternalDocumentApprovedManagerID = internalDocument.ApprovedManager.EmployeeID;
            }
            else if (internalDocument.ApprovedManagerID != 0)
            {
                mInternalDocument.InternalDocumentApprovedManagerID = internalDocument.ApprovedManagerID;
            }
            if (internalDocument.EmployeeRecievedDocument != null) //6
            {
                mInternalDocument.InternalDocumentRecievedEmployeeID = internalDocument.EmployeeRecievedDocument.EmployeeID;
            }
            else if (internalDocument.EmployeeRecievedDocumentID != 0)
            {
                mInternalDocument.InternalDocumentRecievedEmployeeID = internalDocument.EmployeeRecievedDocumentID;
            }
            if (internalDocument.RegistrationDate != null) //7
            {
                mInternalDocument.RegistrationDate = internalDocument.RegistrationDate.ToShortDateString();
            }
            mInternalDocument.InternalDocumentRegistrationNumber = internalDocument.InternalDocumentRegistrationNumber ?? ""; //8
            mInternalDocument.IsRegistated = internalDocument.IsRegistated; //9
            if (internalDocument.SendingDate != null) //10
            {
                mInternalDocument.SendingDate = internalDocument.SendingDate.ToString();
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

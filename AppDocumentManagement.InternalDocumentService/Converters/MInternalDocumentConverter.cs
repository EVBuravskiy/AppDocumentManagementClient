using AppDocumentManagement.Models;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    public class MInternalDocumentConverter
    {

        public static InternalDocument ConvertToInternalDocument(MInternalDocument mInternalDocument)
        {
            InternalDocument internalDocument = new InternalDocument();
            internalDocument.InternalDocumentID = mInternalDocument.InternalDocumentID;
            internalDocument.InternalDocumentType = InternalDocumentTypeConverter.BackConvert(mInternalDocument.InternalDocumentType);
            internalDocument.InternalDocumentDate = DateTime.Parse(mInternalDocument.InternalDocumentDate);
            internalDocument.SignatoryID = mInternalDocument.InternalDocumentSygnatoryID;
            internalDocument.ApprovedManagerID = mInternalDocument.InternalDocumentApprovedManagerID;
            internalDocument.EmployeeRecievedDocumentID = mInternalDocument.InternalDocumentRecievedEmployeeID;
            internalDocument.RegistrationDate = DateTime.Parse(mInternalDocument.RegistrationDate);
            internalDocument.InternalDocumentRegistrationNumber = mInternalDocument.InternalDocumentRegistrationNumber;
            internalDocument.IsRegistated = mInternalDocument.IsRegistated;
            internalDocument.InternalDocumentStatus = DocumentStatusConverter.BackConvert(mInternalDocument.InternalDocumentStatus);
            internalDocument.InternalDocumentTitle = mInternalDocument.InternalDocumentTitle;
            internalDocument.InternalDocumentContent = mInternalDocument.InternalDocumentContent;
            return internalDocument;
        }

        public static MInternalDocument ConvertToMInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = new MInternalDocument();
            mInternalDocument.InternalDocumentID = internalDocument.InternalDocumentID;
            mInternalDocument.InternalDocumentType = InternalDocumentTypeConverter.ToIntConvert(internalDocument.InternalDocumentType);
            mInternalDocument.InternalDocumentDate = internalDocument.InternalDocumentDate.ToShortDateString();
            mInternalDocument.InternalDocumentSygnatoryID = internalDocument.SignatoryID;
            mInternalDocument.InternalDocumentApprovedManagerID = internalDocument.ApprovedManagerID;
            mInternalDocument.InternalDocumentRecievedEmployeeID = internalDocument.EmployeeRecievedDocumentID;
            mInternalDocument.RegistrationDate = internalDocument.RegistrationDate.ToShortDateString();
            mInternalDocument.InternalDocumentRegistrationNumber = internalDocument.InternalDocumentRegistrationNumber;
            mInternalDocument.IsRegistated = internalDocument.IsRegistated;
            mInternalDocument.SendingDate = internalDocument.SendingDate.ToString();
            mInternalDocument.InternalDocumentStatus = DocumentStatusConverter.ToIntConvert(internalDocument.InternalDocumentStatus);
            mInternalDocument.InternalDocumentTitle = internalDocument.InternalDocumentTitle;
            mInternalDocument.InternalDocumentContent = internalDocument.InternalDocumentContent;
            return mInternalDocument;
        }
    }
}

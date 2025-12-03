using AppDocumentManagement.Models;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    public class MExternalDocumentConverter
    {
        public static ExternalDocument ConvertToExternalDocument(MExternalDocument mExternalDocument)
        {
            ExternalDocument externalDocument = new ExternalDocument();
            externalDocument.ExternalDocumentID = mExternalDocument.ExternalDocumentID;
            externalDocument.ExternalDocumentTitle = mExternalDocument.ExternalDocumentTitle;
            externalDocument.ExternalDocumentNumber = mExternalDocument.ExternalDocumentNumber;
            externalDocument.ExternalDocumentDate = DateTime.Parse(mExternalDocument.ExternalDocumentDate);
            externalDocument.ContractorCompanyID = mExternalDocument.ContractorCompanyID;
            externalDocument.ExternalDocumentType = ExternalDocumentTypeConverter.BackConvert(mExternalDocument.ExternalDocumentType);
            externalDocument.RegistrationDate = DateTime.Parse(mExternalDocument.ExternalDocumentRegistrationDate);
            externalDocument.IsRegistated = mExternalDocument.IsRegistrated;
            externalDocument.ReceivingEmployeeID = mExternalDocument.ReceivingEmployeeID;
            externalDocument.ExternalDocumentSendingDate = DateTime.Parse(mExternalDocument.ExternalDocumentSendingDate);
            externalDocument.ExternalDocumentStatus = DocumentStatusConverter.BackConvert(mExternalDocument.ExternalDocumentStatus);
            externalDocument.IsDeleted = mExternalDocument.IsDeleted;
            return externalDocument;
        }

        public static MExternalDocument ConvertToMExternalDocument(ExternalDocument externalDocument)
        {
            MExternalDocument mExternalDocument = new MExternalDocument();
            mExternalDocument.ExternalDocumentID = externalDocument.ExternalDocumentID;
            mExternalDocument.ExternalDocumentTitle = externalDocument.ExternalDocumentTitle;
            mExternalDocument.ExternalDocumentNumber = externalDocument.ExternalDocumentNumber;
            mExternalDocument.ExternalDocumentDate = externalDocument.ExternalDocumentDate.ToShortDateString();
            mExternalDocument.ContractorCompanyID = externalDocument.ContractorCompanyID;
            mExternalDocument.ExternalDocumentType = ExternalDocumentTypeConverter.ToIntConvert(externalDocument.ExternalDocumentType);
            mExternalDocument.ExternalDocumentRegistrationDate = externalDocument.RegistrationDate.ToShortDateString();
            mExternalDocument.IsRegistrated = externalDocument.IsRegistated;
            mExternalDocument.ReceivingEmployeeID = externalDocument.ReceivingEmployeeID ?? 0;
            mExternalDocument.ExternalDocumentSendingDate = externalDocument.ExternalDocumentSendingDate.ToString();
            mExternalDocument.ExternalDocumentStatus = DocumentStatusConverter.ToIntConvert(externalDocument.ExternalDocumentStatus);
            mExternalDocument.IsDeleted = externalDocument.IsDeleted;
            return mExternalDocument;
        }
    }
}

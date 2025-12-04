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
            if (mExternalDocument.ExternalDocumentSendingDate != "")
            {
                externalDocument.ExternalDocumentSendingDate = DateTime.Parse(mExternalDocument.ExternalDocumentSendingDate);
            }
            externalDocument.ExternalDocumentStatus = DocumentStatusConverter.BackConvert(mExternalDocument.ExternalDocumentStatus);
            externalDocument.IsDeleted = mExternalDocument.IsDeleted;
            if (mExternalDocument.ExternalDocumentFiles != null && mExternalDocument.ExternalDocumentFiles.Count > 0)
            {
                externalDocument.ExternalDocumentFiles = new List<ExternalDocumentFile>();
                foreach (MExternalDocumentFile mfile in mExternalDocument.ExternalDocumentFiles)
                {
                    ExternalDocumentFile file = MExternalDocumentFileConverter.ConvertToExternalDocumentFile(mfile);
                    externalDocument.ExternalDocumentFiles.Add(file);
                }
            }
            return externalDocument;
        }

        public static MExternalDocument ConvertToMExternalDocument(ExternalDocument externalDocument)
        {
            MExternalDocument mExternalDocument = new MExternalDocument();
            mExternalDocument.ExternalDocumentID = externalDocument.ExternalDocumentID;
            mExternalDocument.ExternalDocumentTitle = externalDocument.ExternalDocumentTitle;
            mExternalDocument.ExternalDocumentNumber = externalDocument.ExternalDocumentNumber;
            mExternalDocument.ExternalDocumentDate = externalDocument.ExternalDocumentDate.ToShortDateString();
            if (externalDocument.ContractorCompany != null)
            {
                mExternalDocument.ContractorCompanyID = externalDocument.ContractorCompany.ContractorCompanyID;
            }
            else if (externalDocument.ContractorCompanyID != 0)
            {
                mExternalDocument.ContractorCompanyID = externalDocument.ContractorCompanyID;
            }
            mExternalDocument.ExternalDocumentType = ExternalDocumentTypeConverter.ToIntConvert(externalDocument.ExternalDocumentType);
            mExternalDocument.ExternalDocumentRegistrationDate = externalDocument.RegistrationDate.ToShortDateString();
            mExternalDocument.IsRegistrated = externalDocument.IsRegistated;
            if (externalDocument.ReceivingEmployee != null)
            {
                mExternalDocument.ReceivingEmployeeID = externalDocument.ReceivingEmployee.EmployeeID;
            }
            else if (externalDocument.ReceivingEmployeeID != 0)
            {
                mExternalDocument.ReceivingEmployeeID = externalDocument.ReceivingEmployeeID ?? 0;
            }
            if (externalDocument.ExternalDocumentSendingDate != null)
            {
                mExternalDocument.ExternalDocumentSendingDate = externalDocument.ExternalDocumentSendingDate.ToString();
            }
            mExternalDocument.ExternalDocumentStatus = DocumentStatusConverter.ToIntConvert(externalDocument.ExternalDocumentStatus);
            mExternalDocument.IsDeleted = externalDocument.IsDeleted;
            if (externalDocument.ExternalDocumentFiles != null && externalDocument.ExternalDocumentFiles.Count > 0)
            {
                foreach (ExternalDocumentFile file in externalDocument.ExternalDocumentFiles)
                {
                    MExternalDocumentFile mExternalDocumentFile = MExternalDocumentFileConverter.ConvertToMExternalDocumentFile(file);
                    mExternalDocument.ExternalDocumentFiles.Add(mExternalDocumentFile);
                }
            }
            return mExternalDocument;
        }
    }
}

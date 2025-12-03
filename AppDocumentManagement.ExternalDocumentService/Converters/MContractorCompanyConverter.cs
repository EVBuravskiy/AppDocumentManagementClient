using AppDocumentManagement.Models;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    public class MContractorCompanyConverter
    {
        public static ContractorCompany ConvertToContractorCompany(MContractorCompany mContractorCompany)
        {
            ContractorCompany contractorCompany = new ContractorCompany();
            contractorCompany.ContractorCompanyID = mContractorCompany.ContractorCompanyID;
            contractorCompany.ContractorCompanyTitle = mContractorCompany.ContractorCompanyTitle;
            contractorCompany.ContractorCompanyShortTitle = mContractorCompany.ContractorCompanyShortTitle;
            contractorCompany.ContractorCompanyAddress = mContractorCompany.ContractorCompanyAddress;
            contractorCompany.ContractorCompanyPhone = mContractorCompany.ContractorCompanyPhone;
            contractorCompany.ContractorCompanyEmail = mContractorCompany.ContractorCompanyEmail;
            contractorCompany.ContractorCompanyInformation = mContractorCompany.ContractorCompanyInformation;
            contractorCompany.IsDeleted = mContractorCompany.IsDeleted;
            return contractorCompany;
        }

        public static MContractorCompany ConvertToMContractorCompany(ContractorCompany contractorCompany)
        {
            MContractorCompany mContractorCompany = new MContractorCompany();
            mContractorCompany.ContractorCompanyID = contractorCompany.ContractorCompanyID;
            mContractorCompany.ContractorCompanyTitle = contractorCompany.ContractorCompanyTitle;
            mContractorCompany.ContractorCompanyShortTitle = contractorCompany.ContractorCompanyShortTitle;
            mContractorCompany.ContractorCompanyAddress = contractorCompany.ContractorCompanyAddress;
            mContractorCompany.ContractorCompanyPhone = contractorCompany.ContractorCompanyPhone;
            mContractorCompany.ContractorCompanyEmail = contractorCompany.ContractorCompanyEmail;
            mContractorCompany.ContractorCompanyInformation = contractorCompany.ContractorCompanyInformation;
            mContractorCompany.IsDeleted = contractorCompany.IsDeleted;
            return mContractorCompany;
        }
    }
}

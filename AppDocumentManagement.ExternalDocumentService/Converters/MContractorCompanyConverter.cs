using AppDocumentManagement.Models;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    /// <summary>
    /// MContractorCompany Message Converter Class
    /// </summary>
    public class MContractorCompanyConverter
    {
        /// <summary>
        /// Function to convert from MContractorCompany message to ContractorCompany class
        /// </summary>
        /// <param name="mContractorCompany"></param>
        /// <returns>ContractorCompany</returns>
        public static ContractorCompany ConvertToContractorCompany(MContractorCompany mContractorCompany)
        {
            ContractorCompany contractorCompany = new ContractorCompany();
            if (mContractorCompany != null)
            {
                contractorCompany.ContractorCompanyID = mContractorCompany.ContractorCompanyID;
                contractorCompany.ContractorCompanyTitle = mContractorCompany.ContractorCompanyTitle ?? "";
                contractorCompany.ContractorCompanyShortTitle = mContractorCompany.ContractorCompanyShortTitle ?? "";
                contractorCompany.ContractorCompanyAddress = mContractorCompany.ContractorCompanyAddress ?? "";
                contractorCompany.ContractorCompanyPhone = mContractorCompany.ContractorCompanyPhone ?? "";
                contractorCompany.ContractorCompanyEmail = mContractorCompany.ContractorCompanyEmail ?? "";
                contractorCompany.ContractorCompanyInformation = mContractorCompany.ContractorCompanyInformation ?? "";
            }
            return contractorCompany;
        }
        /// <summary>
        /// Function to convert from ContractorCompany class to MContractorCompany message
        /// </summary>
        /// <param name="contractorCompany"></param>
        /// <returns>MContractorCompany</returns>
        public static MContractorCompany ConvertToMContractorCompany(ContractorCompany contractorCompany)
        {
            MContractorCompany mContractorCompany = new MContractorCompany();
            if (contractorCompany != null)
            {
                mContractorCompany.ContractorCompanyID = contractorCompany.ContractorCompanyID;
                mContractorCompany.ContractorCompanyTitle = contractorCompany.ContractorCompanyTitle ?? "";
                mContractorCompany.ContractorCompanyShortTitle = contractorCompany.ContractorCompanyShortTitle ?? "";
                mContractorCompany.ContractorCompanyAddress = contractorCompany.ContractorCompanyAddress ?? "";
                mContractorCompany.ContractorCompanyPhone = contractorCompany.ContractorCompanyPhone ?? "";
                mContractorCompany.ContractorCompanyEmail = contractorCompany.ContractorCompanyEmail ?? "";
                mContractorCompany.ContractorCompanyInformation = contractorCompany.ContractorCompanyInformation ?? "";
            }
            return mContractorCompany;
        }
    }
}

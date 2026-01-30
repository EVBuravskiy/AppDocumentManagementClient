using AppDocumentManagement.ExternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.ExternalDocumentService.Services
{
    /// <summary>
    /// Class of service for sending and receiving ContractorCompany messages
    /// </summary>
    public class ContractorCompanyService
    {
        /// <summary>
        /// Function to add a new contractor company
        /// </summary>
        /// <param name="contractorCompany"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddContractorCompany(ContractorCompany contractorCompany)
        {
            MContractorCompany mContractorCompany = MContractorCompanyConverter.ConvertToMContractorCompany(contractorCompany);
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.AddContractorCompany(mContractorCompany);
            return boolReply.Result;
        }
        /// <summary>
        /// Function to get all contractor companies
        /// </summary>
        /// <returns>List of ContractorCompanys</returns>
        public async Task<List<ContractorCompany>> GetContractorCompanies()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MContractorCompanyList mContractorCompanyList = client.GetContractorCompanies(new EmptyRequest());
            List<ContractorCompany> contractorCompanies = new List<ContractorCompany>();
            foreach (MContractorCompany mContractorCompany in mContractorCompanyList.MContractorCompanyes)
            {
                ContractorCompany contractorCompany = MContractorCompanyConverter.ConvertToContractorCompany(mContractorCompany);
                contractorCompanies.Add(contractorCompany);
            }
            return contractorCompanies;
        }
        /// <summary>
        /// Function to get available contractor companies
        /// </summary>
        /// <returns>List of ContractorCompanys</returns>
        public async Task<List<ContractorCompany>> GetAvailableContractorCompanies()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MContractorCompanyList mContractorCompanyList = client.GetAvailableContractorCompanies(new EmptyRequest());
            List<ContractorCompany> contractorCompanies = new List<ContractorCompany>();
            foreach (MContractorCompany mContractorCompany in mContractorCompanyList.MContractorCompanyes)
            {
                ContractorCompany contractorCompany = MContractorCompanyConverter.ConvertToContractorCompany(mContractorCompany);
                contractorCompanies.Add(contractorCompany);
            }
            return contractorCompanies;
        }
        /// <summary>
        /// Function to get contractor company by ID number
        /// </summary>
        /// <param name="contractorCompanyID"></param>
        /// <returns>ContractorCompany</returns>
        public async Task<ContractorCompany> GetContractorCompanyByID(int contractorCompanyID)
        {
            IDRequest iDRequest = new IDRequest() { ID = contractorCompanyID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MContractorCompany mContractorCompany = client.GetContractorCompanyByID(iDRequest);
            ContractorCompany contractorCompany = MContractorCompanyConverter.ConvertToContractorCompany(mContractorCompany);
            return contractorCompany;
        }
        /// <summary>
        /// Contractor company information update function
        /// </summary>
        /// <param name="contractorCompany"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateContractorCompany(ContractorCompany contractorCompany)
        {
            MContractorCompany mContractorCompany = MContractorCompanyConverter.ConvertToMContractorCompany(contractorCompany);
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.UpdateContractorCompany(mContractorCompany);
            return boolReply.Result;
        }
        /// <summary>
        /// Contractor company delete function
        /// </summary>
        /// <param name="contractorCompanyID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveContractorCompany(int contractorCompanyID)
        {
            IDRequest iDRequest = new IDRequest() { ID = contractorCompanyID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.RemoveContractorCompany(iDRequest);
            return boolReply.Result;
        }
    }
}

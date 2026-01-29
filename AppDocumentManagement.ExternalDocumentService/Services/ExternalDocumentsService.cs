using AppDocumentManagement.ExternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.ExternalDocumentService.Services
{
    /// <summary>
    /// Class of service for sending and receiving ExternalDocument messages
    /// </summary>
    public class ExternalDocumentsService
    {
        /// <summary>
        /// Function for adding a new external document
        /// </summary>
        /// <param name="externalDocument"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddExternalDocument(ExternalDocument externalDocument)
        {
            MExternalDocument mExternalDocument = MExternalDocumentConverter.ConvertToMExternalDocument(externalDocument);
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.AddExternalDocument(mExternalDocument);
            return boolReply.Result;
        }
        /// <summary>
        /// Function to get all external documents
        /// </summary>
        /// <returns>List of external documents</returns>
        public async Task<List<ExternalDocument>> GetAllExternalDocuments()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MExternalDocumentList mExternalDocumentList = client.GetAllExternalDocuments(new EmptyRequest());
            List<ExternalDocument> externalDocuments = new List<ExternalDocument>();
            foreach(MExternalDocument mExternalDocument in mExternalDocumentList.MExternalDocuments)
            {
                ExternalDocument externalDocument = MExternalDocumentConverter.ConvertToExternalDocument(mExternalDocument);
                externalDocuments.Add(externalDocument);
            }
            return externalDocuments;
        }
        /// <summary>
        /// Function to get external document by its ID
        /// </summary>
        /// <param name="externalDocumentID"></param>
        /// <returns>ExternalDocument</returns>
        public async Task<ExternalDocument> GetExternalDocumentsByExternalDocumentID(int externalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = externalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MExternalDocument mExternalDocument = client.GetExternalDocumentsByExternalDocumentID(iDRequest);
            ExternalDocument externalDocument = MExternalDocumentConverter.ConvertToExternalDocument(mExternalDocument);
            return externalDocument;
        }
        /// <summary>
        /// Function for getting external documents by employee ID
        /// </summary>
        /// <param name="recievedEmployeeID"></param>
        /// <returns>List of external documents</returns>
        public async Task<List<ExternalDocument>> GetExternalDocumentsByEmployeeReceivedDocumentID(int recievedEmployeeID)
        {
            IDRequest iDRequest = new IDRequest() { ID = recievedEmployeeID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MExternalDocumentList mExternalDocumentList = client.GetExternalDocumentsByEmployeeReceivedDocumentID(iDRequest);
            List<ExternalDocument> externalDocuments = new List<ExternalDocument>();
            foreach (MExternalDocument mExternalDocument in mExternalDocumentList.MExternalDocuments)
            {
                ExternalDocument externalDocument = MExternalDocumentConverter.ConvertToExternalDocument(mExternalDocument);
                externalDocuments.Add(externalDocument);
            }
            return externalDocuments;
        }
        /// <summary>
        /// External document data update function
        /// </summary>
        /// <param name="externalDocument"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateExternalDocument(ExternalDocument externalDocument)
        {
            MExternalDocument mExternalDocument = MExternalDocumentConverter.ConvertToMExternalDocument(externalDocument);
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.UpdateExternalDocument(mExternalDocument);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for deleting an external document
        /// </summary>
        /// <param name="externalDocumentID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveExternalDocument(int externalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = externalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002");
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.RemoveExternalDocument(iDRequest);
            return boolReply.Result;
        }
    }
}

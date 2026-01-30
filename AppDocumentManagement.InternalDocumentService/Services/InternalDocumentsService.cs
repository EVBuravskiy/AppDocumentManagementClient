using AppDocumentManagement.InternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.InternalDocumentService.Services
{
    /// <summary>
    /// Class of service for sending and receiving InternalDocument messages
    /// </summary>
    public class InternalDocumentsService
    {
        /// <summary>
        /// Function for adding a new internal document
        /// </summary>
        /// <param name="internalDocument"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = MInternalDocumentConverter.ConvertToMInternalDocument(internalDocument);
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.AddInternalDocument(mInternalDocument);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for obtaining all internal documents
        /// </summary>
        /// <returns>List of InternalDocuments</returns>
        public async Task<List<InternalDocument>> GetInternalDocuments()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            MInternalDocumentList mInternalDocumentList = client.GetInternalDocuments(new EmptyRequest());
            List<InternalDocument> internalDocuments = new List<InternalDocument>();
            foreach (MInternalDocument mInternalDocument in mInternalDocumentList.MIntrernalDocuments)
            {
                InternalDocument internalDocument = MInternalDocumentConverter.ConvertToInternalDocument(mInternalDocument);
                internalDocuments.Add(internalDocument);
            }
            return internalDocuments;
        }
        /// <summary>
        /// Function for obtaining an internal document by its ID
        /// </summary>
        /// <param name="internalDocumentID"></param>
        /// <returns>InternalDocument</returns>
        public async Task<InternalDocument> GetInternalDocumentsByInternalDocumentID(int internalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = internalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            MInternalDocument mInternalDocument = client.GetInternalDocumentsByInternalDocumentID(iDRequest);
            InternalDocument internalDocument = MInternalDocumentConverter.ConvertToInternalDocument(mInternalDocument);
            return internalDocument;
        }
        /// <summary>
        /// Function for obtaining internal documents by employee ID
        /// </summary>
        /// <param name="recievedEmployeeID"></param>
        /// <returns>List of InternalDocuments</returns>
        public async Task<List<InternalDocument>> GetInternalDocumentsByEmployeeRecievedDocumentID(int recievedEmployeeID)
        {
            IDRequest iDRequest = new IDRequest() { ID = recievedEmployeeID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            MInternalDocumentList mInternalDocumentList = client.GetInternalDocumentsByEmployeeRecievedDocumentID(iDRequest);
            List<InternalDocument> internalDocuments = new List<InternalDocument>();
            foreach (MInternalDocument mInternalDocument in mInternalDocumentList.MIntrernalDocuments)
            {
                InternalDocument internalDocument = MInternalDocumentConverter.ConvertToInternalDocument(mInternalDocument);
                internalDocuments.Add(internalDocument);
            }
            return internalDocuments;
        }
        /// <summary>
        /// Internal document data update function
        /// </summary>
        /// <param name="internalDocument"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = MInternalDocumentConverter.ConvertToMInternalDocument(internalDocument);
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.UpdateInternalDocument(mInternalDocument);
            return boolReply.Result;
        }
        /// <summary>
        /// Function to remove an internal document
        /// </summary>
        /// <param name="internalDocumentID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveInternalDocument(int internalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = internalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.RemoveInternalDocument(iDRequest);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for obtaining the number of internal documents by their type
        /// </summary>
        /// <param name="internalDocumentType"></param>
        /// <returns>int</returns>
        public int GetCountInternalDocumentByType(InternalDocumentType internalDocumentType)
        {
            List<InternalDocument> internalDocuments = GetInternalDocuments().Result;
            List<InternalDocument> internalDocumentsByType = internalDocuments.Where(d => d.InternalDocumentType == internalDocumentType).ToList();
            return internalDocumentsByType.Count;
        }
    }
}

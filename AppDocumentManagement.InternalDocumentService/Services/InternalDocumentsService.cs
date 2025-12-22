using AppDocumentManagement.InternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;
using System.Collections.Generic;

namespace AppDocumentManagement.InternalDocumentService.Services
{
    public class InternalDocumentsService
    {
        public async Task<bool> AddInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = MInternalDocumentConverter.ConvertToMInternalDocument(internalDocument);
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.AddInternalDocument(mInternalDocument);
            return boolReply.Result;
        }

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

        public async Task<InternalDocument> GetInternalDocumentsByInternalDocumentID(int internalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = internalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            MInternalDocument mInternalDocument = client.GetInternalDocumentsByInternalDocumentID(iDRequest);
            InternalDocument internalDocument = MInternalDocumentConverter.ConvertToInternalDocument(mInternalDocument);
            return internalDocument;
        }

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

        public async Task<bool> UpdateInternalDocument(InternalDocument internalDocument)
        {
            MInternalDocument mInternalDocument = MInternalDocumentConverter.ConvertToMInternalDocument(internalDocument);
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.UpdateInternalDocument(mInternalDocument);
            return boolReply.Result;
        }

        public async Task<bool> RemoveInternalDocument(int internalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = internalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003");
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.RemoveInternalDocument(iDRequest);
            return boolReply.Result;
        }

        public int GetCountInternalDocumentByType(InternalDocumentType internalDocumentType)
        {
            List<InternalDocument> internalDocuments = GetInternalDocuments().Result;
            List<InternalDocument> internalDocumentsByType = internalDocuments.Where(d => d.InternalDocumentType == internalDocumentType).ToList();
            return internalDocumentsByType.Count;
        }
    }
}

using AppDocumentManagement.InternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.InternalDocumentService.Services
{
    public class InternalDocumentFileService
    {
        public async Task<bool> AddInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            MInternalDocumentFile mInternalDocumentFile = MInternalDocumentFileConverter.ConvertToMInternalDocumentFile(internalDocumentFile);
            using var channel = GrpcChannel.ForAddress("http://localhost:6003", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024 
            });
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.AddInternalDocumentFile(mInternalDocumentFile);
            return boolReply.Result;
        }

        public async Task<bool> AddInternalDocumentFiles(List<InternalDocumentFile> internalDocumentFiles)
        {
            MInternalDocumentFileList mInternalDocumentFileList = new MInternalDocumentFileList();
            foreach (InternalDocumentFile internalDocumentFile in internalDocumentFiles)
            {
                MInternalDocumentFile mInternalDocumentFile = MInternalDocumentFileConverter.ConvertToMInternalDocumentFile(internalDocumentFile);
                mInternalDocumentFileList.MInternalDocumentFiles.Add(mInternalDocumentFile);
            }
            using var channel = GrpcChannel.ForAddress("http://localhost:6003", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.AddInternalDocumentFiles(mInternalDocumentFileList);
            return boolReply.Result;
        }
        public async Task<List<InternalDocumentFile>> GetInternalDocumentFiles(int internalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = internalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            MInternalDocumentFileList mInternalDocumentFileList = client.GetInternalDocumentFiles(iDRequest);
            List<InternalDocumentFile> internalDocumentFiles = new List<InternalDocumentFile>();
            foreach(MInternalDocumentFile mInternalDocumentFile in mInternalDocumentFileList.MInternalDocumentFiles)
            {
                InternalDocumentFile internalDocumentFile = MInternalDocumentFileConverter.ConvertToInternalDocumentFile(mInternalDocumentFile);
                internalDocumentFiles.Add(internalDocumentFile);
            }
            return internalDocumentFiles;
        }

        public async Task<bool> RemoveInternalDocumentFile(int internalDocumentFileID)
        {
            IDRequest iDRequest = new IDRequest() { ID= internalDocumentFileID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6003", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new internalDocumentAPI.internalDocumentAPIClient(channel);
            var boolReply = client.RemoveInternalDocumentFile(iDRequest);
            return boolReply.Result;
        }
    }
}

using AppDocumentManagement.InternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.InternalDocumentService.Services
{
    /// <summary>
    /// Class of service for sending and receiving InternalDocumentFile messages
    /// </summary>
    public class InternalDocumentFileService
    {
        /// <summary>
        /// Function to add a new file to an internal document
        /// </summary>
        /// <param name="internalDocumentFile"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Function for adding files to an internal document
        /// </summary>
        /// <param name="internalDocumentFiles"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Function for obtaining internal document files by its ID
        /// </summary>
        /// <param name="internalDocumentID"></param>
        /// <returns>List of internal document files</returns>
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
        /// <summary>
        /// Function to remove a file from an internal document
        /// </summary>
        /// <param name="internalDocumentFileID"></param>
        /// <returns>bool</returns>
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

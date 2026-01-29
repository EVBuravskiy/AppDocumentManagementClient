using AppDocumentManagement.ExternalDocumentService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.ExternalDocumentService.Services
{
    /// <summary>
    /// Class of service for sending and receiving ExternalDocumentFile messages
    /// </summary>
    public class ExternalDocumentFileService
    {
        /// <summary>
        /// Function for adding file to an external document
        /// </summary>
        /// <param name="externalDocumentFile"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddExternalDocumentFile(ExternalDocumentFile externalDocumentFile)
        {
            MExternalDocumentFile mExternalDocumentFile = MExternalDocumentFileConverter.ConvertToMExternalDocumentFile(externalDocumentFile);
            using var channel = GrpcChannel.ForAddress("http://localhost:6002", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.AddExternalDocumentFile(mExternalDocumentFile);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for adding files to an external document
        /// </summary>
        /// <param name="externalDocumentFiles"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddExternalDocumentFiles(List<ExternalDocumentFile> externalDocumentFiles)
        {
            MExternalDocumentFileList mExternalDocumentFileList = new MExternalDocumentFileList();
            foreach (ExternalDocumentFile file in externalDocumentFiles)
            {
                MExternalDocumentFile mExternalDocumentFile = MExternalDocumentFileConverter.ConvertToMExternalDocumentFile(file);
                mExternalDocumentFileList.MEsternalDocumentFiles.Add(mExternalDocumentFile);
            }
            using var channel = GrpcChannel.ForAddress("http://localhost:6002", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.AddExternalDocumentFiles(mExternalDocumentFileList);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for obtaining external document files
        /// </summary>
        /// <param name="externalDocumentID"></param>
        /// <returns>List of external document files</returns>
        public async Task<List<ExternalDocumentFile>> GetExternalDocumentFiles(int externalDocumentID)
        {
            IDRequest iDRequest = new IDRequest() { ID = externalDocumentID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 100 * 1024 * 1024
            });
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            MExternalDocumentFileList mExternalDocumentFileList = client.GetExternalDocumentFiles(iDRequest);
            List<ExternalDocumentFile> externalDocumentFiles = new List<ExternalDocumentFile>();
            foreach (MExternalDocumentFile file in mExternalDocumentFileList.MEsternalDocumentFiles)
            {
                ExternalDocumentFile externalDocumentFile = MExternalDocumentFileConverter.ConvertToExternalDocumentFile(file);
                externalDocumentFiles.Add(externalDocumentFile);
            }
            return externalDocumentFiles;
        }
        /// <summary>
        /// Function for remove file from external document
        /// </summary>
        /// <param name="externalDocumentFileID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveExternalDocumentFile(int externalDocumentFileID)
        {
            IDRequest iDRequest = new IDRequest() { ID= externalDocumentFileID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6002", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new externalDocumentAPI.externalDocumentAPIClient(channel);
            var boolReply = client.RemoveExternalDocumentFile(iDRequest);
            return boolReply.Result;
        }
    }
}

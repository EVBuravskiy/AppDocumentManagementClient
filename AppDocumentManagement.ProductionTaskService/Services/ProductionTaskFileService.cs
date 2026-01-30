using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Converters;
using Grpc.Net.Client;

namespace AppDocumentManagement.ProductionTaskService.Services
{
    /// <summary>
    /// Class of service for sending and receiving ProductionTaskFile messages
    /// </summary>
    public class ProductionTaskFileService
    {
        /// <summary>
        /// Function for adding a new file to a task
        /// </summary>
        /// <param name="productionTaskFile"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductionTaskFile(ProductionTaskFile productionTaskFile)
        {
            MProductionTaskFile mProductionTaskFile = MProductionTaskFileConverter.ConvertToMProductionTaskFile(productionTaskFile);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTaskFile(mProductionTaskFile);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for adding files to a task
        /// </summary>
        /// <param name="productionTaskFiles"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductionTaskFiles(List<ProductionTaskFile> productionTaskFiles)
        {
            MProductionTaskFileList mProductionTaskFileList = new MProductionTaskFileList();
            foreach (ProductionTaskFile productionTaskFile in productionTaskFiles)
            {
                MProductionTaskFile mProductionTaskFile = MProductionTaskFileConverter.ConvertToMProductionTaskFile(productionTaskFile);
                mProductionTaskFileList.MProductionTaskFiles.Add(mProductionTaskFile);
            }
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTaskFiles(mProductionTaskFileList);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for getting task files
        /// </summary>
        /// <param name="productionTaskID"></param>
        /// <returns>List of ProductionTaskFiles</returns>
        public async Task<List<ProductionTaskFile>> GetProductionTaskFiles(int productionTaskID)
        {
            IDRequest iDRequest = new IDRequest() { ID = productionTaskID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            MProductionTaskFileList mProductionTaskFileList = client.GetProductionTaskFiles(iDRequest);
            List<ProductionTaskFile> productionTaskFiles = new List<ProductionTaskFile>();
            foreach (MProductionTaskFile mProductionTaskFile in mProductionTaskFileList.MProductionTaskFiles)
            {
                ProductionTaskFile productionTaskFile = MProductionTaskFileConverter.ConvertToProductionTaskFile(mProductionTaskFile);
                productionTaskFiles.Add(productionTaskFile);
            }
            return productionTaskFiles;
        }
        /// <summary>
        /// Function to remove a task file.
        /// </summary>
        /// <param name="productionTaskFile"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveProductionTaskFile(ProductionTaskFile productionTaskFile)
        {
            IDRequest iDRequest = new IDRequest() { ID = productionTaskFile.ProductionTaskFileID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.RemoveProductionTaskFile(iDRequest);
            return boolReply.Result;
        }
    }
}

using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Converters;
using Grpc.Net.Client;

namespace AppDocumentManagement.ProductionTaskService.Services
{
    public class ProductionTaskFileService
    {
        public async Task<bool> AddProductionTaskFile(ProductionTaskFile productionTaskFile)
        {
            MProductionTaskFile mProductionTaskFile = MProductionTaskFileConverter.ConvertToMProductionTaskFile(productionTaskFile);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTaskFile(mProductionTaskFile);
            return boolReply.Result;
        }

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

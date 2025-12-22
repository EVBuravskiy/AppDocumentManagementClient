using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Converters;
using Grpc.Net.Client;

namespace AppDocumentManagement.ProductionTaskService.Services
{
    public class ProductionTaskSubService
    {
        public async Task<bool> AddProductionSubTask(ProductionSubTask productionSubTask)
        {
            MProductionSubTask mProductionSubTask = MProductionSubTaskConverter.ConvertToMProductionSubTask(productionSubTask);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionSubTask(mProductionSubTask);
            return boolReply.Result;
        }

        public async Task<bool> AddProductionSubTasks(List<ProductionSubTask> productionSubTasks)
        {
            MProductionSubTaskList mProductionSubTaskList = new MProductionSubTaskList();
            foreach (ProductionSubTask productionSubTask in productionSubTasks)
            {
                MProductionSubTask mProductionSubTask = MProductionSubTaskConverter.ConvertToMProductionSubTask(productionSubTask);
                mProductionSubTaskList.MProductionSubTasks.Add(mProductionSubTask);
            }
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionSubTasks(mProductionSubTaskList);
            return boolReply.Result;
        }

        public async Task<List<ProductionSubTask>> GetProductionSubTasks(int productionTaskID)
        {
            IDRequest iDRequest = new IDRequest() { ID = productionTaskID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            MProductionSubTaskList mProductionSubTaskList = client.GetProductionSubTasks(iDRequest);
            List<ProductionSubTask> productionSubTasks = new List<ProductionSubTask>();
            foreach (MProductionSubTask mProductionSubTask in mProductionSubTaskList.MProductionSubTasks)
            {
                ProductionSubTask productionSubTask = MProductionSubTaskConverter.ConvertToProductionSubTask(mProductionSubTask);
                productionSubTasks.Add(productionSubTask);
            }
            return productionSubTasks;
        }

        public async Task<bool> UpdateProductionSubTask(ProductionSubTask productionSubTask)
        {
            MProductionSubTask mProductionSubTask = MProductionSubTaskConverter.ConvertToMProductionSubTask(productionSubTask);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.UpdateProductionSubTask(mProductionSubTask);
            return boolReply.Result;
        }
    }
}

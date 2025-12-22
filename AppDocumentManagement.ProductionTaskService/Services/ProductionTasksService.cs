using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Converters;
using Grpc.Net.Client;

namespace AppDocumentManagement.ProductionTaskService.Services
{
    public class ProductionTasksService
    {
        public async Task<bool> AddProductionTask(ProductionTask productionTask)
        {
            MProductionTask mProductionTask = MProductionTaskConverter.ConvertToMProductionTask(productionTask);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTask(mProductionTask);
            return boolReply.Result;
        }

        public async Task<List<ProductionTask>> GetProductionTasks()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            MProductionTaskList mProductionTaskList = client.GetProductionTasks(new EmptyRequest());
            List<ProductionTask> productionTasks = new List<ProductionTask>();
            foreach (MProductionTask mProductionTask in mProductionTaskList.MProductionTasks)
            {
                ProductionTask productionTask = MProductionTaskConverter.ConvertToProductionTask(mProductionTask);
                productionTasks.Add(productionTask);
            }
            return productionTasks;
        }

        public async Task<List<ProductionTask>> GetProductionTasksByEmployeeID(int employeeID)
        {
            IDRequest iDRequest = new IDRequest() { ID = employeeID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            MProductionTaskList mProductionTaskList = client.GetProductionTasksByEmployeeID(iDRequest);
            List<ProductionTask> productionTasks = new List<ProductionTask>();
            foreach (MProductionTask mProductionTask in mProductionTaskList.MProductionTasks)
            {
                ProductionTask productionTask = MProductionTaskConverter.ConvertToProductionTask(mProductionTask);
                productionTasks.Add(productionTask);
            }
            return productionTasks;
        }

        public async Task<List<ProductionTask>> GetProductionTasksByCreatorID(int creatorID)
        {
            IDRequest iDRequest = new IDRequest() { ID = creatorID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            MProductionTaskList mProductionTaskList = client.GetProductionTasksByCreatorID(iDRequest);
            List<ProductionTask> productionTasks = new List<ProductionTask>();
            foreach (MProductionTask mProductionTask in mProductionTaskList.MProductionTasks)
            {
                ProductionTask productionTask = MProductionTaskConverter.ConvertToProductionTask(mProductionTask);
                productionTasks.Add(productionTask);
            }
            return productionTasks;
        }

        public async Task<bool> UpdateProductionTaskStatus(ProductionTask productionTask)
        {
            MProductionTask mProductionTask = MProductionTaskConverter.ConvertToMProductionTask(productionTask);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.UpdateProductionTaskStatus(mProductionTask);
            return boolReply.Result;
        }
    }
}

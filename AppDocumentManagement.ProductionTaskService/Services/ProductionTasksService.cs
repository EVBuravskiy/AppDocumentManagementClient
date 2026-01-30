using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Converters;
using Grpc.Net.Client;

namespace AppDocumentManagement.ProductionTaskService.Services
{
    /// <summary>
    /// Class of service for sending and receiving ProductionTask messages
    /// </summary>
    public class ProductionTasksService
    {
        /// <summary>
        /// Function to add a new task
        /// </summary>
        /// <param name="productionTask"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductionTask(ProductionTask productionTask)
        {
            MProductionTask mProductionTask = MProductionTaskConverter.ConvertToMProductionTask(productionTask);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTask(mProductionTask);
            return boolReply.Result;
        }
        /// <summary>
        /// Add tasks function
        /// </summary>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Receiving tasks function
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>List of ProductionTasks</returns>
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
        /// <summary>
        /// Function for receiving tasks by employee ID
        /// </summary>
        /// <param name="creatorID"></param>
        /// <returns>List of ProductionTasks</returns>
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
        /// <summary>
        /// Task status update function
        /// </summary>
        /// <param name="productionTask"></param>
        /// <returns>bool</returns>
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

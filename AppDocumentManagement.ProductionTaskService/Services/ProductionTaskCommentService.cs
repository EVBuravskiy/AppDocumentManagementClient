using AppDocumentManagement.Models;
using AppDocumentManagement.ProductionTaskService.Converters;
using Grpc.Net.Client;

namespace AppDocumentManagement.ProductionTaskService.Services
{
    /// <summary>
    /// Class of service for sending and receiving ProductionTaskComment messages
    /// </summary>
    public class ProductionTaskCommentService
    {
        /// <summary>
        /// Function for adding a comment to a task
        /// </summary>
        /// <param name="productionTaskComment"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductionTaskComment(ProductionTaskComment productionTaskComment)
        {
            MProductionTaskComment mProductionTaskComment = MProductionTaskCommentConverter.ConvertToMProductionTaskComment(productionTaskComment);
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTaskComment(mProductionTaskComment);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for adding comments to a task
        /// </summary>
        /// <param name="productionTaskComments"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductionTaskComments(List<ProductionTaskComment> productionTaskComments)
        {
            MProductionTaskCommentList mProductionTaskCommentList = new MProductionTaskCommentList();
            foreach(ProductionTaskComment productionTaskComment in productionTaskComments)
            {
                MProductionTaskComment mProductionTaskComment = MProductionTaskCommentConverter.ConvertToMProductionTaskComment(productionTaskComment);
                mProductionTaskCommentList.MProductionTaskComments.Add(mProductionTaskComment);
            }
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            var boolReply = client.AddProductionTaskComments(mProductionTaskCommentList);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for getting comments to a task.
        /// </summary>
        /// <param name="productionTaskID"></param>
        /// <returns>List of ProductionTaskComments</returns>
        public async Task<List<ProductionTaskComment>> GetProductionTaskComments(int productionTaskID)
        {
            IDRequest iDRequest = new IDRequest() { ID = productionTaskID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6004");
            var client = new productionTaskAPI.productionTaskAPIClient(channel);
            MProductionTaskCommentList mProductionTaskCommentList = client.GetProductionTaskComments(iDRequest);
            List<ProductionTaskComment> productionTaskComments = new List<ProductionTaskComment>();
            foreach (MProductionTaskComment mProductionTaskComment in mProductionTaskCommentList.MProductionTaskComments)
            {
                ProductionTaskComment productionTaskComment = MProductionTaskCommentConverter.ConvertToProductionTaskComment(mProductionTaskComment);
                productionTaskComments.Add(productionTaskComment);
            }
            return productionTaskComments;
        }
    }
}

using AppDocumentManagement.Models;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    /// <summary>
    /// MProductionTaskComment Message Converter Class
    /// </summary>
    public class MProductionTaskCommentConverter
    {
        /// <summary>
        /// Function to convert from MProductionTaskComment message to ProductionTaskComment class
        /// </summary>
        /// <param name="mProductionTaskComment"></param>
        /// <returns>ProductionTaskComment</returns>
        public static ProductionTaskComment ConvertToProductionTaskComment(MProductionTaskComment mProductionTaskComment)
        {
            ProductionTaskComment productionTaskComment = new ProductionTaskComment();
            if (mProductionTaskComment.ProductionTaskCommentID != 0)
            {
                productionTaskComment.ProductionTaskCommentID = mProductionTaskComment.ProductionTaskCommentID;
            }
            if (DateTime.TryParse(mProductionTaskComment.ProductionTaskCommentDate, out DateTime commentDate))
            {
                productionTaskComment.ProductionTaskCommentDate = commentDate;
            }
            productionTaskComment.ProductionTaskCommentText = mProductionTaskComment.ProductionTaskCommentText;
            productionTaskComment.ProductionTaskID = mProductionTaskComment.ProductionTaskID;
            productionTaskComment.EmployeeID = mProductionTaskComment.EmployeeID;
            return productionTaskComment;
        }
        /// <summary>
        /// Function to convert from ProductionTaskComment class to MProductionTaskComment message
        /// </summary>
        /// <param name="productionTaskComment"></param>
        /// <returns>MProductionTaskComment</returns>
        public static MProductionTaskComment ConvertToMProductionTaskComment(ProductionTaskComment productionTaskComment)
        {
            MProductionTaskComment mProductionTaskComment = new MProductionTaskComment();
            if (productionTaskComment.ProductionTaskCommentID != 0)
            {
                mProductionTaskComment.ProductionTaskCommentID = productionTaskComment.ProductionTaskCommentID;
            }
            mProductionTaskComment.ProductionTaskCommentDate = productionTaskComment.ProductionTaskCommentDate.ToShortDateString();
            mProductionTaskComment.ProductionTaskCommentText = productionTaskComment.ProductionTaskCommentText;
            mProductionTaskComment.ProductionTaskID = productionTaskComment.ProductionTaskID;
            mProductionTaskComment.EmployeeID = productionTaskComment.EmployeeID;
            return mProductionTaskComment;
        }

    }
}

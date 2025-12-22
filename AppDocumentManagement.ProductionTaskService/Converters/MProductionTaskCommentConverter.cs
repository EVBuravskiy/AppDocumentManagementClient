using AppDocumentManagement.Models;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    public class MProductionTaskCommentConverter
    {
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

        public static MProductionTaskComment ConvertToMProductionTaskComment(ProductionTaskComment productionTaskComment)
        {
            MProductionTaskComment mProductionTaskComment = new MProductionTaskComment();
            if (productionTaskComment.ProductionTaskCommentID != 0)
            {
                mProductionTaskComment.ProductionTaskCommentID = productionTaskComment.ProductionTaskCommentID;
            }
            if (productionTaskComment.ProductionTaskCommentDate != null)
            {
                mProductionTaskComment.ProductionTaskCommentDate = productionTaskComment.ProductionTaskCommentDate.ToShortDateString();
            }
            mProductionTaskComment.ProductionTaskCommentText = productionTaskComment.ProductionTaskCommentText;
            mProductionTaskComment.ProductionTaskID = productionTaskComment.ProductionTaskID;
            mProductionTaskComment.EmployeeID = productionTaskComment.EmployeeID;
            return mProductionTaskComment;
        }

    }
}

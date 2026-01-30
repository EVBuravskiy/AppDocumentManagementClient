using AppDocumentManagement.Models;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    /// <summary>
    /// MProductionSubTask Message Converter Class
    /// </summary>
    public class MProductionSubTaskConverter
    {
        /// <summary>
        /// Function to convert from MProductionSubTask message to ProductionSubTask class
        /// </summary>
        /// <param name="mProductionSubTask"></param>
        /// <returns>ProductionSubTask</returns>
        public static ProductionSubTask ConvertToProductionSubTask(MProductionSubTask mProductionSubTask)
        {
            ProductionSubTask productionSubTask = new ProductionSubTask();
            if (mProductionSubTask.ProductionSubTaskID != 0)
            {
                productionSubTask.ProductionSubTaskID = mProductionSubTask.ProductionSubTaskID;
            }
            productionSubTask.ProductionSubTaskDescription = mProductionSubTask.ProductionSubTaskDescription;
            if (DateTime.TryParse(mProductionSubTask.ProductionSubTaskCreateTime, out DateTime createTime))
            {
                productionSubTask.ProductionSubTaskCreateTime = createTime;
            }
            productionSubTask.ProductionTaskID = mProductionSubTask.ProductionTaskID;
            productionSubTask.IsDone = mProductionSubTask.IsDone;
            return productionSubTask;
        }
        /// <summary>
        /// Function to convert from ProductionSubTask class to MProductionSubTask message
        /// </summary>
        /// <param name="productionSubTask"></param>
        /// <returns>MProductionSubTask</returns>
        public static MProductionSubTask ConvertToMProductionSubTask(ProductionSubTask productionSubTask)
        {
            MProductionSubTask mProductionSubTask = new MProductionSubTask();
            if (productionSubTask.ProductionSubTaskID != 0)
            {
                mProductionSubTask.ProductionSubTaskID = productionSubTask.ProductionSubTaskID;
            }
            mProductionSubTask.ProductionSubTaskDescription = productionSubTask.ProductionSubTaskDescription;
            mProductionSubTask.ProductionSubTaskCreateTime = productionSubTask.ProductionSubTaskCreateTime.ToShortDateString();
            mProductionSubTask.ProductionTaskID = productionSubTask.ProductionTaskID;
            mProductionSubTask.IsDone = productionSubTask.IsDone;
            return mProductionSubTask;
        }
    }
}

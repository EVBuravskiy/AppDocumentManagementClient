using AppDocumentManagement.Models;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    public class MProductionSubTaskConverter
    {
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

        public static MProductionSubTask ConvertToMProductionSubTask(ProductionSubTask productionSubTask)
        {
            MProductionSubTask mProductionSubTask = new MProductionSubTask();
            if (productionSubTask.ProductionSubTaskID != 0)
            {
                mProductionSubTask.ProductionSubTaskID = productionSubTask.ProductionSubTaskID;
            }
            mProductionSubTask.ProductionSubTaskDescription = productionSubTask.ProductionSubTaskDescription;
            if (productionSubTask.ProductionSubTaskCreateTime != null)
            {
                mProductionSubTask.ProductionSubTaskCreateTime = productionSubTask.ProductionSubTaskCreateTime.ToShortDateString();
            }
            mProductionSubTask.ProductionTaskID = productionSubTask.ProductionTaskID;
            mProductionSubTask.IsDone = productionSubTask.IsDone;
            return mProductionSubTask;
        }
    }
}

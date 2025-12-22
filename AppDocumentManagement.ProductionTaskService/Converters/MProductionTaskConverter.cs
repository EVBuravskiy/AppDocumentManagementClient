using AppDocumentManagement.Models;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    public class MProductionTaskConverter
    {
        public static ProductionTask ConvertToProductionTask(MProductionTask mProductionTask)
        {
            ProductionTask productionTask = new ProductionTask();
            if (mProductionTask.ProductionTaskID != 0)
            {
                productionTask.ProductionTaskID = mProductionTask.ProductionTaskID;
            }
            productionTask.ProductionTaskTitle = mProductionTask.ProductionTaskTitle;
            productionTask.Priority = mProductionTask.Priority;
            productionTask.ExternalDocumentID = mProductionTask.ExternalDocumentID;
            productionTask.InternalDocumentID = mProductionTask.InternalDocumentID;
            if (mProductionTask.ProductionTaskCreateDate != string.Empty)
            {
                productionTask.ProductionTaskCreateDate = DateTime.Parse(mProductionTask.ProductionTaskCreateDate);
            }
            if (mProductionTask.ProductionTaskDueDate != string.Empty)
            {
                productionTask.ProductionTaskDueDate = DateTime.Parse(mProductionTask.ProductionTaskDueDate);
            }
            productionTask.ProductionTaskDescription = mProductionTask.ProductionTaskDescription;
            productionTask.EmployeesID = new List<int>();
            if (mProductionTask.EmployeesID != null && mProductionTask.EmployeesID.Count > 0)
            {
                foreach (int employeeID in mProductionTask.EmployeesID)
                {
                    productionTask.EmployeesID.Add(employeeID);
                }
            }
            productionTask.EmployeeCreatorID = mProductionTask.EmployeeCreatorID;
            productionTask.ProductionTaskStatus = ProductionTaskStatusConverter.BackConvert(mProductionTask.ProductionTaskStatus);
            productionTask.ProductionSubTasks = new List<ProductionSubTask>();
            if (mProductionTask.MProductionSubTasks != null && mProductionTask.MProductionSubTasks.MProductionSubTasks.Count > 0)
            {
                foreach (MProductionSubTask subTask in mProductionTask.MProductionSubTasks.MProductionSubTasks)
                {
                    ProductionSubTask productionSubTask = MProductionSubTaskConverter.ConvertToProductionSubTask(subTask);
                    productionTask.ProductionSubTasks.Add(productionSubTask);
                }
            }
            productionTask.ProductionTaskComments = new List<ProductionTaskComment>();
            if (mProductionTask.MProductionTaskComments != null && mProductionTask.MProductionTaskComments.MProductionTaskComments.Count > 0)
            {
                foreach (MProductionTaskComment taskComment in mProductionTask.MProductionTaskComments.MProductionTaskComments)
                {
                    ProductionTaskComment productionTaskComment = MProductionTaskCommentConverter.ConvertToProductionTaskComment(taskComment);
                    productionTask.ProductionTaskComments.Add(productionTaskComment);
                }
            }
            productionTask.ProductionTaskFiles = new List<ProductionTaskFile>();
            if (mProductionTask.MProductionTaskFiles != null && mProductionTask.MProductionTaskFiles.MProductionTaskFiles.Count > 0)
            {
                foreach (MProductionTaskFile taskFile in mProductionTask.MProductionTaskFiles.MProductionTaskFiles)
                {
                    ProductionTaskFile productionTaskFile = MProductionTaskFileConverter.ConvertToProductionTaskFile(taskFile);
                    productionTask.ProductionTaskFiles.Add(productionTaskFile);
                }
            }
            return productionTask;
        }

        public static MProductionTask ConvertToMProductionTask(ProductionTask productionTask)
        {
            MProductionTask mProductionTask = new MProductionTask();
            if (productionTask.ProductionTaskID != 0)
            {
                mProductionTask.ProductionTaskID = productionTask.ProductionTaskID;
            }
            mProductionTask.ProductionTaskTitle = productionTask.ProductionTaskTitle;
            mProductionTask.Priority = productionTask.Priority;
            mProductionTask.ExternalDocumentID = productionTask.ExternalDocumentID;
            mProductionTask.InternalDocumentID = productionTask.InternalDocumentID;
            if (productionTask.ProductionTaskCreateDate != null)
            {
                mProductionTask.ProductionTaskCreateDate = productionTask.ProductionTaskCreateDate.ToShortDateString();
            }
            if (productionTask.ProductionTaskDueDate != null)
            {
                mProductionTask.ProductionTaskDueDate = productionTask.ProductionTaskDueDate.ToShortDateString();
            }
            mProductionTask.ProductionTaskDescription = productionTask.ProductionTaskDescription;
            if (productionTask.EmployeesID != null && productionTask.EmployeesID.Count > 0)
            {
                foreach (int employeeID in productionTask.EmployeesID)
                {
                    mProductionTask.EmployeesID.Add(employeeID);
                }
            }
            mProductionTask.EmployeeCreatorID = productionTask.EmployeeCreatorID;
            mProductionTask.ProductionTaskStatus = ProductionTaskStatusConverter.ToIntConvert(productionTask.ProductionTaskStatus);
            mProductionTask.MProductionSubTasks = new MProductionSubTaskList(); //добавил создание лист
            if (productionTask.ProductionSubTasks != null && productionTask.ProductionSubTasks.Count > 0)
            {
                foreach (ProductionSubTask subTask in productionTask.ProductionSubTasks)
                {
                    MProductionSubTask mProductionSubTask = MProductionSubTaskConverter.ConvertToMProductionSubTask(subTask);
                    mProductionTask.MProductionSubTasks.MProductionSubTasks.Add(mProductionSubTask);
                }
            }
            mProductionTask.MProductionTaskComments = new MProductionTaskCommentList();
            if (productionTask.ProductionTaskComments != null && productionTask.ProductionTaskComments.Count > 0)
            {
                foreach (ProductionTaskComment taskComment in productionTask.ProductionTaskComments)
                {
                    MProductionTaskComment mProductionTaskComment = MProductionTaskCommentConverter.ConvertToMProductionTaskComment(taskComment);
                    mProductionTask.MProductionTaskComments.MProductionTaskComments.Add(mProductionTaskComment);
                }
            }
            mProductionTask.MProductionTaskFiles = new MProductionTaskFileList();
            if (productionTask.ProductionTaskFiles != null && productionTask.ProductionTaskFiles.Count > 0)
            {
                foreach (ProductionTaskFile taskFile in productionTask.ProductionTaskFiles)
                {
                    MProductionTaskFile mProductionTaskFile = MProductionTaskFileConverter.ConvertToMProductionTaskFile(taskFile);
                    mProductionTask.MProductionTaskFiles.MProductionTaskFiles.Add(mProductionTaskFile);
                }
            }
            return mProductionTask;
        }
    }
}

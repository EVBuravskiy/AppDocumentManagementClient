using AppDocumentManagement.Models;
using Google.Protobuf;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    public class MProductionTaskFileConverter
    {
        public static ProductionTaskFile ConvertToProductionTaskFile(MProductionTaskFile mProductionTaskFile)
        {
            ProductionTaskFile productionTaskFile = new ProductionTaskFile();
            if (mProductionTaskFile.ProductionTaskFileID != 0)
            {
                productionTaskFile.ProductionTaskFileID = mProductionTaskFile.ProductionTaskFileID;
            }
            productionTaskFile.ProductionTaskFileName = mProductionTaskFile.ProductionTaskFileName;
            productionTaskFile.ProductionTaskFileExtension = mProductionTaskFile.ProductionTaskFileExtension;
            productionTaskFile.ProductionTaskFileData = mProductionTaskFile.ProductionTaskFileData.ToByteArray();
            if (mProductionTaskFile.ProductionTaskID != 0)
            {
                productionTaskFile.ProductionTaskID = mProductionTaskFile.ProductionTaskID;
            }
            return productionTaskFile;
        }

        public static MProductionTaskFile ConvertToMProductionTaskFile(ProductionTaskFile productionTaskFile)
        {
            MProductionTaskFile mProductionTaskFile = new MProductionTaskFile();
            if (productionTaskFile.ProductionTaskFileID != 0)
            {
                mProductionTaskFile.ProductionTaskFileID = productionTaskFile.ProductionTaskFileID;
            }
            mProductionTaskFile.ProductionTaskFileName = productionTaskFile.ProductionTaskFileName;
            mProductionTaskFile.ProductionTaskFileExtension = productionTaskFile.ProductionTaskFileExtension;
            mProductionTaskFile.ProductionTaskFileData = ByteString.CopyFrom(productionTaskFile.ProductionTaskFileData);
            if (productionTaskFile.ProductionTask != null)
            {
                mProductionTaskFile.ProductionTaskID = productionTaskFile.ProductionTask.ProductionTaskID;
            }
            else if (productionTaskFile.ProductionTaskID != 0)
            {
                mProductionTaskFile.ProductionTaskID = productionTaskFile.ProductionTaskID;
            }
            return mProductionTaskFile;
        }
    }
}

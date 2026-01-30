using AppDocumentManagement.Models;

namespace AppDocumentManagement.ProductionTaskService.Converters
{
    /// <summary>
    /// ProductionTaskStatus Enumeration Converter Class
    /// </summary>
    public class ProductionTaskStatusConverter
    {
        /// <summary>
        /// Function to convert from ProductionTaskStatus enum to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                ProductionTaskStatus.InProgress => 0,
                ProductionTaskStatus.UnderInspection => 1,
                ProductionTaskStatus.Done => 2,
                _ => 0,
            };
        }
        /// <summary>
        /// Function to convert from int to ProductionTaskStatus enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ProductionTaskStatus BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => ProductionTaskStatus.InProgress,
                1 => ProductionTaskStatus.UnderInspection,
                2 => ProductionTaskStatus.Done,
            };
        }
    }
}

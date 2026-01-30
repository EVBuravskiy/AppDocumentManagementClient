using AppDocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Task status converter class
    /// </summary>
    [ValueConversion(typeof(ProductionTaskStatus), typeof(String))]
    public class TaskStatusConverter : IValueConverter
    {
        /// <summary>
        /// Function to convert ProductionTaskStatus to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is ProductionTaskStatus status)
                {
                    return status switch
                    {
                        ProductionTaskStatus.InProgress => "В работе",
                        ProductionTaskStatus.UnderInspection => "На проверке",
                        ProductionTaskStatus.Done => "Выполнено"
                    };
                }
            }
            return value;
        }
        /// <summary>
        /// Function to convert string to ProductionTaskStatus
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>ProductionTaskStatus</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            ProductionTaskStatus status = ProductionTaskStatus.InProgress;
            if (strValue != null)
            {
                return strValue switch
                {
                    "В работе" => ProductionTaskStatus.InProgress,
                    "На проверке" => ProductionTaskStatus.UnderInspection,
                    "Выполнено" => ProductionTaskStatus.Done
                };
            }
            return status;
        }
    }
}

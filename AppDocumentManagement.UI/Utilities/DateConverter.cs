using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Date converter for UI
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateConverter : IValueConverter
    {
        /// <summary>
        /// Function to convert date to string
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
                if (value is DateTime date)
                {
                    return date.ToLongDateString();
                }
            }
            return value;
        }
        /// <summary>
        /// Function to convert string to date
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>DateTime</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
        /// <summary>
        /// Function to convert date to string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertDateToString(DateTime date)
        {
            return date.ToLongDateString();
        }
    }
}

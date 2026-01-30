using System.Globalization;
using System.Windows.Data;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Importance converter class for UI
    /// </summary>
    [ValueConversion(typeof(String), typeof(Boolean))]
    public class ImportanceConverter : IValueConverter
    {
        /// <summary>
        /// Function to convert bool value of importance to string
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
                if (value is bool importance)
                {
                    if (importance == true)
                    {
                        value = "ВАЖНО!!!";
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// Function to convert string to bool of importance
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>bool</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            bool importance = false;
            if (strValue != null)
            {
                if (strValue == "ВАЖНО!!!") { importance = true; }
                else { importance = false; }
            }
            return importance;
        }
    }

}

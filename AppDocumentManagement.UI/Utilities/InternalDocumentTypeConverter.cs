using AppDocumentManagement.Models;
using System.Globalization;
using System.Windows.Data;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// InternalDocumentType converter class for UI
    /// </summary>
    public class InternalDocumentTypeConverter : IValueConverter
    {
        /// <summary>
        /// Function to convert InternalDocumentType to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ConvertToString(object value)
        {
            if (value is InternalDocumentType type)
            {
                return type switch
                {
                    InternalDocumentType.Order => "Приказ",
                    InternalDocumentType.Direction => "Указание/распоряжение",
                    InternalDocumentType.Report => "Рапорт",
                    InternalDocumentType.OfficialLetter => "Служебная записка",
                };
            }
            return value.ToString();
        }
        /// <summary>
        /// Function to convert string to InternalDocumentType
        /// </summary>
        /// <param name="value"></param>
        /// <returns>InternalDocumentType</returns>
        public static InternalDocumentType ConvertToEnum(string value)
        {
            return value switch
            {
                "Приказ" => InternalDocumentType.Order,
                "Указание/распоряжение" => InternalDocumentType.Direction,
                "Рапорт" => InternalDocumentType.Report,
                "Служебная записка" => InternalDocumentType.OfficialLetter,
            };
        }
        /// <summary>
        /// Function to convert InternalDocumentType to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                InternalDocumentType.Order => 0,
                InternalDocumentType.Direction => 1,
                InternalDocumentType.Report => 2,
                InternalDocumentType.OfficialLetter => 3,
                _ => 0,
            };
        }
        /// <summary>
        /// Function to convert int to InternalDocumentType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static InternalDocumentType BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => InternalDocumentType.Order,
                1 => InternalDocumentType.Direction,
                2 => InternalDocumentType.Report,
                3 => InternalDocumentType.OfficialLetter,
                _ => InternalDocumentType.Order
            };
        }
        /// <summary>
        /// Default function to convert InternalDocumentType to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToString(value);
        }
        /// <summary>
        /// Default function to convert InternalDocumentType to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToString(value);
        }
    }
}

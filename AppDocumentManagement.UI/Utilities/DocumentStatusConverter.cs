using AppDocumentManagement.Models;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Document status converter for UI
    /// </summary>
    public class DocumentStatusConverter
    {
        /// <summary>
        /// Function to convert DocumentStatus to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ConvertToString(object value)
        {
            if (value is DocumentStatus externalDocumentStatus)
            {
                return externalDocumentStatus switch
                {
                    DocumentStatus.UnderConsideration => "На рассмотрении",
                    DocumentStatus.Agreed => "Согласованo",
                    DocumentStatus.Refused => "Отказанo"
                };
            }
            return value.ToString();
        }
        /// <summary>
        /// Function to convert DocumentStatus to string
        /// </summary>
        /// <param name="documentStatus"></param>
        /// <returns>string</returns>
        public static string ConvertEnumToString(DocumentStatus documentStatus)
        {
            return documentStatus switch
            {
                DocumentStatus.UnderConsideration => "На рассмотрении",
                DocumentStatus.Agreed => "Согласованo",
                DocumentStatus.Refused => "Отказанo"
            };
        }
        /// <summary>
        /// Function to convert string to DocumentStatus
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DocumentStatus</returns>
        public static DocumentStatus ConvertToEnum(string value)
        {
            return value switch
            {
                "На рассмотрении" => DocumentStatus.UnderConsideration,
                "Согласованo" => DocumentStatus.Agreed,
                "Отказанo" => DocumentStatus.Refused,
            };
        }
        /// <summary>
        /// Function to convert DocumentStatus enum to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                DocumentStatus.UnderConsideration => 0,
                DocumentStatus.Agreed => 1,
                DocumentStatus.Refused => 2,
                _ => 0,
            };
        }
        /// <summary>
        /// Function to convert int to DocumentStatus enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DocumentStatus BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => DocumentStatus.UnderConsideration,
                1 => DocumentStatus.Agreed,
                2 => DocumentStatus.Refused,
            };
        }
    }
}

using AppDocumentManagement.Models;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    /// <summary>
    /// DocumentStatus Enumeration Converter Class
    /// </summary>
    public class DocumentStatusConverter
    {
        /// <summary>
        /// Function to convert from DocumentStatus enum to int
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
        /// Function to convert from int to DocumentStatus enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DocumentStatus</returns>
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

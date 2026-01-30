using AppDocumentManagement.Models;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    /// <summary>
    /// InternalDocumentType Enumeration Converter Class
    /// </summary>
    public class InternalDocumentTypeConverter
    {
        /// <summary>
        /// Function to convert from InternalDocumentType enum to int
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
        /// Function to convert from int to InternalDocumentType enum
        /// </summary>
        /// <param name="inputvalue"></param>
        /// <returns>InternalDocumentType</returns>
        public static InternalDocumentType BackConvert(int inputvalue)
        {
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
    }
}

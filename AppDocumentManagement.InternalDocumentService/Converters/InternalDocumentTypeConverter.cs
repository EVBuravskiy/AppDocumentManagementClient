using AppDocumentManagement.Models;

namespace AppDocumentManagement.InternalDocumentService.Converters
{
    public class InternalDocumentTypeConverter
    {
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
    }
}

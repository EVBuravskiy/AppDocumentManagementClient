using AppDocumentManagement.Models;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    public class DocumentStatusConverter
    {
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

using AppDocumentManagement.Models;

namespace AppDocumentManagement.ExternalDocumentService.Converters
{
    public class ExternalDocumentTypeConverter
    {
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                ExternalDocumentType.Contract => 0,
                ExternalDocumentType.CommercialOffer => 1,
                ExternalDocumentType.Letter => 2,
                ExternalDocumentType.GovernmentLetter => 3,
                _ => 0,
            };
        }

        public static ExternalDocumentType BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => ExternalDocumentType.Contract,
                1 => ExternalDocumentType.CommercialOffer,
                2 => ExternalDocumentType.Letter,
                3 => ExternalDocumentType.GovernmentLetter,
                _ => ExternalDocumentType.Contract
            };
        }
    }
}

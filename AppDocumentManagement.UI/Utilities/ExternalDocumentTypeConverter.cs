using AppDocumentManagement.Models;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// External document type converter class
    /// </summary>
    public class ExternalDocumentTypeConverter
    {
        /// <summary>
        /// Function to convert ExternalDocumentType enum to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ConvertToString(object value)
        {
            if (value is ExternalDocumentType type)
            {
                return type switch
                {
                    ExternalDocumentType.Contract => "Договор/контракт",
                    ExternalDocumentType.CommercialOffer => "Коммерческое предложение",
                    ExternalDocumentType.Letter => "Письмо/Сопроводительное письмо",
                    ExternalDocumentType.GovernmentLetter => "Представление/Требование",
                };
            }
            return value.ToString();
        }
        /// <summary>
        /// Function to convert string to ExternalDocumentType enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ExternalDocumentType ConvertToEnum(string value)
        {
            return value switch
            {
                "Договор/контракт" => ExternalDocumentType.Contract,
                "Коммерческое предложение" => ExternalDocumentType.CommercialOffer,
                "Письмо/Сопроводительное письмо" => ExternalDocumentType.Letter,
                "Представление/Требование" => ExternalDocumentType.GovernmentLetter,
                _ => ExternalDocumentType.Contract,
            };
        }
        /// <summary>
        /// Function to convert ExternalDocumentType enum to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
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
        /// <summary>
        /// Function to convert int to ExternalDocumentType enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

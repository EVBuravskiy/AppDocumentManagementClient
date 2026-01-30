namespace AppDocumentManagement.Models
{
    /// <summary>
    /// Contractor company class
    /// </summary>
    public class ContractorCompany
    {
        public int ContractorCompanyID { get; set; }
        public string ContractorCompanyTitle { get; set; }
        public string? ContractorCompanyShortTitle { get; set; }
        public string ContractorCompanyAddress { get; set; }
        public string? ContractorCompanyPhone { get; set; }
        public string? ContractorCompanyEmail { get; set; }
        List<ExternalDocument> ExternalDocuments { get; set; } = new List<ExternalDocument>();
        public string? ContractorCompanyInformation { get; set; }
    }
}

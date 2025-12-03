namespace AppDocumentManagement.Models
{
    public class ContractorCompany
    {
        public int ContractorCompanyID { get; set; }
        public string ContractorCompanyTitle { get; set; }
        public string? ContractorCompanyShortTitle { get; set; }
        public string ContractorCompanyAddress { get; set; }
        public string? ContractorCompanyPhone { get; set; }
        public string? ContractorCompanyEmail { get; set; }
        public string? ContractorCompanyInformation { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

namespace AppDocumentManagement.Models
{
    /// <summary>
    /// External document class
    /// </summary>
    public class ExternalDocument
    {
        public int ExternalDocumentID { get; set; }
        public string ExternalDocumentTitle { get; set; }
        public string? ExternalDocumentNumber { get; set; }
        public DateTime ExternalDocumentDate { get; set; }
        public ContractorCompany ContractorCompany { get; set; }
        public int ContractorCompanyID { get; set; }
        public ExternalDocumentType ExternalDocumentType { get; set; }
        public List<ExternalDocumentFile>? ExternalDocumentFiles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationNumber => ExternalDocumentID.ToString();
        public bool IsRegistered { get; set; }
        public Employee? ReceivingEmployee { get; set; }
        public int? ReceivingEmployeeID { get; set; }
        public DateTime? ExternalDocumentSendingDate { get; set; }
        public DocumentStatus ExternalDocumentStatus { get; set; }
        public ProductionTask? ProductionTask { get; set; }
        public int ProductionTaskID { get; set; } = 0;

    }
}

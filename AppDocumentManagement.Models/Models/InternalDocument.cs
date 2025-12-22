namespace AppDocumentManagement.Models
{
    public class InternalDocument
    {
        public int InternalDocumentID { get; set; }
        public InternalDocumentType InternalDocumentType { get; set; }
        public DateTime InternalDocumentDate { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee? Signatory { get; set; }
        public int SignatoryID { get; set; }
        public Employee? ApprovedManager { get; set; }
        public int ApprovedManagerID { get; set; }
        public Employee? EmployeeRecievedDocument { get; set; }
        public int EmployeeRecievedDocumentID { get; set; }
        public List<InternalDocumentFile>? InternalDocumentFiles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? InternalDocumentRegistrationNumber { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime? InternalDocumentSendingDate { get; set; }
        public DocumentStatus InternalDocumentStatus { get; set; }
        public string InternalDocumentTitle { get; set; } = string.Empty;
        public string InternalDocumentContent { get; set; } = string.Empty;
        public ProductionTask? ProductionTask { get; set; }
        public int ProductionTaskID { get; set; } = 0;
    }
}

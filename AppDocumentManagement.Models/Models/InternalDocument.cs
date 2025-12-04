namespace AppDocumentManagement.Models
{
    public class InternalDocument
    {
        public int InternalDocumentID { get; set; }
        public InternalDocumentType InternalDocumentType { get; set; }
        public DateTime InternalDocumentDate { get; set; }
        public int SignatoryID { get; set; }
        public int ApprovedManagerID { get; set; }
        public int EmployeeRecievedDocumentID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? InternalDocumentRegistrationNumber { get; set; }
        public bool IsRegistated { get; set; }
        public DateTime? SendingDate { get; set; }
        public DocumentStatus InternalDocumentStatus { get; set; }
        public string InternalDocumentTitle { get; set; }
        public string InternalDocumentContent { get; set; }
        public Employee Signatory {  get; set; }
        public Employee ApprovedManager { get; set; }
        public Employee EmployeeRecievedDocument { get; set; }
    }
}

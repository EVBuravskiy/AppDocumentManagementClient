using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocumentManagement.Models
{
    /// <summary>
    /// Task comment class
    /// </summary>
    public class ProductionTaskComment
    {
        public int ProductionTaskCommentID { get; set; }
        public DateTime ProductionTaskCommentDate { get; set; }
        public string ProductionTaskCommentText { get; set; } = string.Empty;
        public ProductionTask? ProductionTask { get; set; }
        public int ProductionTaskID { get; set; }
        [NotMapped]
        public Employee? Employee { get; set; }
        public int EmployeeID { get; set; }
    }
}

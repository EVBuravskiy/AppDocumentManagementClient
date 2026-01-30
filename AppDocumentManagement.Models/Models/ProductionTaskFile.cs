namespace AppDocumentManagement.Models
{ 
    /// <summary>
    /// Task file class
    /// </summary>
    public class ProductionTaskFile
    {
        public int ProductionTaskFileID { get; set; }
        public string ProductionTaskFileName { get; set; }
        public string ProductionTaskFileExtension { get; set; }
        public byte[] ProductionTaskFileData { get; set; }
        public ProductionTask ProductionTask { get; set; }
        public int ProductionTaskID { get; set; }
    }
}

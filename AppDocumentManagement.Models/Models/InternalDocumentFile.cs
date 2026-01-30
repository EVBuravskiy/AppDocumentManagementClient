namespace AppDocumentManagement.Models
{
    /// <summary>
    /// Internal document file class
    /// </summary>
    public class InternalDocumentFile
    {
        public int InternalDocumentFileID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileData { get; set; }
        public InternalDocument InternalDocument { get; set; }
        public int InternalDocumentID { get; set; }

    }
}

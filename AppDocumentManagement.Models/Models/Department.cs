namespace AppDocumentManagement.Models
{
    /// <summary>
    /// Department class
    /// </summary>
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentTitle { get; set; }
        public string DepartmentShortTitle { get; set; }
        public List<Employee> Employees { get; set; }
    }
}

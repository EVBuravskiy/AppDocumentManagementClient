namespace AppDocumentManagement.EmployeeService.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? EmployeeFirstName { get; set; }
        public string? EmployeeLastName { get; set; }
        public string? EmployeeMiddleName { get; set; }
        public int DepartmentID { get; set; }
        public string? Position { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public string EmployeeFullName => $"{EmployeeLastName} {EmployeeFirstName} {EmployeeMiddleName}";
        public string EmployeeShortName => $"{EmployeeLastName} {EmployeeFirstName.ElementAt(0)}.{EmployeeMiddleName.ElementAt(0)}.";
        public string? EmployeePhone { get; set; }
        public string? EmployeeEmail { get; set; }
        public string? EmployeeInformation { get; set; }
        public string EmployeeFirstMiddleName => $"{EmployeeFirstName} {EmployeeMiddleName}";
        public bool IsDeleted { get; set; } = false;
    }
}

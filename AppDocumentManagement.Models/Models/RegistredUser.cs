namespace AppDocumentManagement.Models
{
    public class RegistredUser
    {
        public int RegistredUserID { get; set; }
        public string RegistredUserLogin { get; set; } = string.Empty;
        public string RegistredUserPassword { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public DateTime UserRegistrationTime { get; set; }
        public Employee? Employee { get; set; }
        public int EmployeeID { get; set; }
        public bool IsRegistered { get; set; } = false;
    }
}

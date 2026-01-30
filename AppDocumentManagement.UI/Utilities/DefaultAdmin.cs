using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.EmployeesService.Services;
using AppDocumentManagement.Models;
using System.Windows;


namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Default administration class
    /// </summary>
    public class DefaultAdmin
    {
        /// <summary>
        /// Function to create default administrator
        /// </summary>
        /// <returns>bool</returns>
        public static bool CreateDefaultAdmin()
        {
            Department defaultDepartment = CreateDefaultDepartment();
            Employee defaultEmployee = CreateDefaultEmployee(defaultDepartment);
            bool result = RegistredDefaultAdmin(defaultEmployee);
            if (result)
            {
                MessageBox.Show("Создан администратор по умолчанию");
                return true;
            }
            else
            {
                MessageBox.Show("Не удалось создать дефолтного администратора");
                return false;
            }
        }
        /// <summary>
        /// Function to create default department
        /// </summary>
        /// <returns>Department</returns>
        private static Department CreateDefaultDepartment()
        {
            Department defaultDepartment = new Department();
            defaultDepartment.DepartmentTitle = "Default";
            defaultDepartment.DepartmentShortTitle = "Default";
            DepartmentService departmentService = new DepartmentService();
            bool result = departmentService.AddDepartment(defaultDepartment).Result;
            if (!result)
            {
                MessageBox.Show("Ошибка создания дефолтного отдела");
                return null;
            }
            return defaultDepartment;
        }
        /// <summary>
        /// Function to create default employee
        /// </summary>
        /// <param name="defaultDepartment"></param>
        /// <returns>Employee</returns>
        private static Employee CreateDefaultEmployee(Department defaultDepartment)
        {
            Employee defaultEmployee = new Employee();
            defaultEmployee.EmployeeFirstName = "Default";
            defaultEmployee.EmployeeLastName = "Default";
            defaultEmployee.EmployeeMiddleName = "Default";
            defaultEmployee.EmployeeRole = EmployeeRole.Performer;
            DepartmentService departmentService = new DepartmentService();
            List<Department> departments = departmentService.GetAllDepartments().Result;
            Department department = departments.FirstOrDefault();
            defaultEmployee.DepartmentID = department.DepartmentID;
            EmployesService employesService = new EmployesService();
            bool result = employesService.AddEmployee(defaultEmployee).Result;
            return defaultEmployee;
        }
        /// <summary>
        /// Function for registering the default administrator
        /// </summary>
        /// <param name="defaultEmployee"></param>
        /// <returns>bool</returns>
        private static bool RegistredDefaultAdmin(Employee defaultEmployee)
        {
            RegistredUser registredUser = new RegistredUser();
            registredUser.RegistredUserLogin = "Admin01";
            registredUser.UserRegistrationTime = DateTime.Now;
            string password = $"{registredUser.RegistredUserLogin}-01Nimda";
            registredUser.RegistredUserPassword = PassHasher.CalculateMD5Hash(password);
            registredUser.IsRegistered = true;
            EmployesService employesService = new EmployesService();
            List<Employee> employees = employesService.GetAllEmployees().Result;
            Employee employee = employees.FirstOrDefault();
            registredUser.EmployeeID = employee.EmployeeID;
            registredUser.UserRole = UserRole.Administrator;
            RegisterUserService registerUserService = new RegisterUserService();
            return registerUserService.AddRegistratedUser(registredUser).Result;
        }
    }
}

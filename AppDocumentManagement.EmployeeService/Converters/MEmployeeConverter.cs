using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeesService.Converters
{
    /// <summary>
    /// MEmployee Message Converter Class
    /// </summary>
    public class MEmployeeConverter
    {
        /// <summary>
        /// Function to convert from MEmployee message to Employee class
        /// </summary>
        /// <param name="mEmployee"></param>
        /// <returns>Employee</returns>
        public static Employee ConvertToEmployee(MEmployee mEmployee)
        {
            Employee employee = new Employee();
            employee.EmployeeID = mEmployee.EmployeeID;
            employee.EmployeeFirstName = mEmployee.EmployeeFirstName;
            employee.EmployeeLastName = mEmployee.EmployeeLastName;
            employee.EmployeeMiddleName = mEmployee.EmployeeMiddleName;
            employee.DepartmentID = mEmployee.DepartmentID;
            employee.Position = mEmployee.Position;
            employee.EmployeeRole = EmployeeRoleConverter.BackConvert(mEmployee.EmployeeRole);
            employee.EmployeePhone = mEmployee.EmployeePhone;
            employee.EmployeeEmail = mEmployee.EmployeeEmail;
            employee.EmployeeInformation = mEmployee.EmployeeInformation;
            if (mEmployee.EmployeePhoto != null)
            {
                employee.EmployeePhoto = MEmployeePhotoConverter.ConvertToEmployeePhoto(mEmployee.EmployeePhoto);
            }
            return employee;
        }
        /// <summary>
        /// Function to convert from Employee class to MEmployee message
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>MEmployee</returns>
        public static MEmployee ConvertToMEmployee(Employee employee)
        {
            MEmployee mEmployee = new MEmployee();
            mEmployee.EmployeeID = employee.EmployeeID;
            mEmployee.EmployeeFirstName = employee.EmployeeFirstName ?? "";
            mEmployee.EmployeeLastName = employee.EmployeeLastName ?? "";
            mEmployee.EmployeeMiddleName = employee.EmployeeMiddleName ?? "";
            mEmployee.DepartmentID = employee.DepartmentID;
            mEmployee.Position = employee.Position ?? "";
            mEmployee.EmployeeRole = EmployeeRoleConverter.ToIntConvert(employee.EmployeeRole);
            mEmployee.EmployeePhone = employee.EmployeePhone ?? "";
            mEmployee.EmployeeEmail = employee.EmployeeEmail ?? "";
            mEmployee.EmployeeInformation = employee.EmployeeInformation ?? "";
            if (employee.EmployeePhoto != null)
            {
                mEmployee.EmployeePhoto = MEmployeePhotoConverter.ConvertToMEmployeePhoto(employee.EmployeePhoto);
            }
            return mEmployee;
        }
    }
}

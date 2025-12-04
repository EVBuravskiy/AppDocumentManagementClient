using AppDocumentManagement.EmployeeService.Service;
using AppDocumentManagement.Models;


namespace AppDocumentManagement.UI.Utilities
{
    public class DepartmentConverter
    {
        public static string DepartmentToString(Department department)
        {
            return department.DepartmentTitle;
        }

        public static Department StringToDepartment(string departmentTitle)
        {
            DepartmentService departmentService = new DepartmentService();
            List<Department> departments = departmentService.GetAllDepartments().Result;
            Department department = departments.SingleOrDefault(d => d.DepartmentTitle == departmentTitle);
            return department;
        }

        public static int DepartmentToInt(Department department, List<Department> departments)
        {
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].DepartmentID == department.DepartmentID)
                {
                    return i;
                }
            }
            return 0;
        }

        public static Department IntToDepartment(int index, List<Department> departments)
        {
            return departments[index];
        }
    }
}

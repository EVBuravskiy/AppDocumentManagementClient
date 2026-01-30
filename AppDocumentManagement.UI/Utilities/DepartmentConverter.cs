using AppDocumentManagement.EmployeesService.Service;
using AppDocumentManagement.Models;


namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Class for department conversion
    /// </summary>
    public class DepartmentConverter
    {
        /// <summary>
        /// Function to convert Department to string
        /// </summary>
        /// <param name="department"></param>
        /// <returns>string</returns>
        public static string DepartmentToString(Department department)
        {
            return department.DepartmentTitle;
        }
        /// <summary>
        /// Function to get Department by title
        /// </summary>
        /// <param name="departmentTitle"></param>
        /// <returns>Department</returns>
        public static Department StringToDepartment(string departmentTitle)
        {
            DepartmentService departmentService = new DepartmentService();
            List<Department> departments = departmentService.GetAllDepartments().Result;
            Department department = departments.SingleOrDefault(d => d.DepartmentTitle == departmentTitle);
            return department;
        }
        /// <summary>
        /// Function to get index of department
        /// </summary>
        /// <param name="department"></param>
        /// <param name="departments"></param>
        /// <returns>int</returns>
        public static int DepartmentToInt(Department department, List<Department> departments)
        {
            if (department == null) return 0;
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].DepartmentID == department.DepartmentID)
                {
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        /// Function to get Department by index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="departments"></param>
        /// <returns>Department</returns>
        public static Department IntToDepartment(int index, List<Department> departments)
        {
            return departments[index];
        }
    }
}

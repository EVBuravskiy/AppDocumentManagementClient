using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeesService.Converters
{
    /// <summary>
    /// MDepartment Message Converter Class
    /// </summary>
    public class MDepartmentConverter
    {
        /// <summary>
        /// Function to convert from MDepartment message to Department class
        /// </summary>
        /// <param name="mDepartment"></param>
        /// <returns>Department</returns>
        public static Department ConvertToDepartment(MDepartment mDepartment)
        {
            Department department = new Department();
            if (mDepartment.DepartmentID != 0)
            {
                department.DepartmentID = mDepartment.DepartmentID;
            }
            department.DepartmentTitle = mDepartment.DepartmentTitle;
            department.DepartmentShortTitle = mDepartment.DepartmentShortTitle;
            return department;
        }
        /// <summary>
        /// Function to convert from Department class to MDepartment message
        /// </summary>
        /// <param name="department"></param>
        /// <returns>MDepartment</returns>
        public static MDepartment ConvertToMDepartment(Department department)
        {
            MDepartment mDepartment = new MDepartment();
            if (department.DepartmentID != 0)
            {
                mDepartment.DepartmentID = department.DepartmentID;
            }
            mDepartment.DepartmentTitle = department.DepartmentTitle;
            mDepartment.DepartmentShortTitle = department.DepartmentShortTitle;
            return mDepartment;
        }
    }
}

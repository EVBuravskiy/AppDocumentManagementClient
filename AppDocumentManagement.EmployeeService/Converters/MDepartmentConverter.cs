using AppDocumentManagement.EmployeeService.Models;

namespace AppDocumentManagement.EmployeeService.Converters
{
    public class MDepartmentConverter
    {
        public static Department ConvertToDepartment(MDepartment mDepartment)
        {
            Department department = new Department();
            department.DepartmentID = mDepartment.DepartmentID;
            department.DepartmentTitle = mDepartment.DepartmentTitle;
            department.DepartmentShortTitle = mDepartment.DepartmentShortTitle;
            return department;
        }

        public static MDepartment ConvertToMDepartment(Department department)
        {
            MDepartment mDepartment = new MDepartment();
            mDepartment.DepartmentID = department.DepartmentID;
            mDepartment.DepartmentTitle = department.DepartmentTitle;
            mDepartment.DepartmentShortTitle = department.DepartmentShortTitle;
            return mDepartment;
        }
    }
}

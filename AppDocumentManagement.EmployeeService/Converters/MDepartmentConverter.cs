using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeesService.Converters
{
    public class MDepartmentConverter
    {
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

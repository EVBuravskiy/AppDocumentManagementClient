using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeesService.Converters
{
    public class EmployeeRoleConverter
    {
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                EmployeeRole.GeneralDirector => 0,
                EmployeeRole.DeputyGeneralDirector => 1,
                EmployeeRole.HeadOfDepartment => 2,
                EmployeeRole.Performer => 3,
            };
        }

        public static EmployeeRole BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => EmployeeRole.GeneralDirector,
                1 => EmployeeRole.DeputyGeneralDirector,
                2 => EmployeeRole.HeadOfDepartment,
                3 => EmployeeRole.Performer,
                _ => EmployeeRole.Performer,
            };
        }

    }
}

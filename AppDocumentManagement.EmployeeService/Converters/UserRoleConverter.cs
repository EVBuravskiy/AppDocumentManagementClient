using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeeService.Converters
{
    public class UserRoleConverter
    {
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                UserRole.Administrator => 0,
                UserRole.GeneralDirector => 1,
                UserRole.DeputyGeneralDirector => 2,
                UserRole.HeadOfDepartment => 3,
                UserRole.Performer => 4,
                UserRole.Сlerk => 5,
            };
        }

        public static UserRole BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 4;
            return inputvalue switch
            {
                0 => UserRole.Administrator,
                1 => UserRole.GeneralDirector,
                2 => UserRole.DeputyGeneralDirector,
                3 => UserRole.HeadOfDepartment,
                4 => UserRole.Performer,
                5 => UserRole.Сlerk,
                _ => UserRole.Performer,
            };
        }
    }
}

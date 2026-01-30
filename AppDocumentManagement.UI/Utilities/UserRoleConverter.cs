using AppDocumentManagement.Models;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// User role converter class
    /// </summary>
    public class UserRoleConverter
    {
        /// <summary>
        /// Function to convert UserRole to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ConvertToString(object value)
        {
            if (value is UserRole role)
            {
                return role switch
                {
                    UserRole.Administrator => "Администратор",
                    UserRole.GeneralDirector => "Генеральный директор",
                    UserRole.DeputyGeneralDirector => "Заместитель генерального директора",
                    UserRole.HeadOfDepartment => "Начальник отдела",
                    UserRole.Performer => "Специалист",
                    UserRole.Сlerk => "Делопроизводитель",
                    UserRole.PersonnelOfficer => "Работник отдела кадров",
                    _ => "Специалист",
                };
            }
            return value.ToString();
        }
        /// <summary>
        /// Function to convert string to UserRole
        /// </summary>
        /// <param name="value"></param>
        /// <returns>UserRole</returns>
        public static UserRole ConvertToEnum(string value)
        {
            return value switch
            {
                "Администратор" => UserRole.Administrator,
                "Генеральный директор" => UserRole.GeneralDirector,
                "Заместитель генерального директора" => UserRole.DeputyGeneralDirector,
                "Начальник отдела" => UserRole.HeadOfDepartment,
                "Специалист" => UserRole.Performer,
                "Делопроизводитель" => UserRole.Сlerk,
                "Работник отдела кадров" => UserRole.PersonnelOfficer,
                _ => UserRole.Performer,
            };
        }
        /// <summary>
        /// Function to convert UserRole to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
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
                UserRole.PersonnelOfficer => 6,
                _ => 4,
            };
        }
        /// <summary>
        /// Function to convert int to UserRole
        /// </summary>
        /// <param name="value"></param>
        /// <returns>UserRole</returns>
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
                6 => UserRole.PersonnelOfficer,
                _ => UserRole.Performer,
            };
        }
    }
}


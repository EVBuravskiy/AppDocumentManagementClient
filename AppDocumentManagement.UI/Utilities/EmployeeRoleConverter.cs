using AppDocumentManagement.Models;

namespace AppDocumentManagement.UI.Utilities
{
    /// <summary>
    /// Employee role converter class
    /// </summary>
    public class EmployeeRoleConverter
    {
        /// <summary>
        /// Function to convert EmployeeRole to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ConvertToString(object value)
        {
            if (value is EmployeeRole role)
            {
                return role switch
                {
                    EmployeeRole.GeneralDirector => "Генеральный директор",
                    EmployeeRole.DeputyGeneralDirector => "Заместитель генерального директора",
                    EmployeeRole.HeadOfDepartment => "Начальник отдела",
                    EmployeeRole.Performer => "Исполнитель"
                };
            }
            return value.ToString();
        }
        /// <summary>
        /// Function to convert string to EmployeeRole
        /// </summary>
        /// <param name="value"></param>
        /// <returns>EmployeeRole</returns>
        public static EmployeeRole ConvertToEnum(string value)
        {
            return value switch
            {
                "Генеральный директор" => EmployeeRole.GeneralDirector,
                "Заместитель генерального директора" => EmployeeRole.DeputyGeneralDirector,
                "Начальник отдела" => EmployeeRole.HeadOfDepartment,
                "Исполнитель" => EmployeeRole.Performer
            };
        }
        /// <summary>
        /// Function to convert EmployeeRole enum to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>int</returns>
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
        /// <summary>
        /// Function to convert int to EmployeeRole enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

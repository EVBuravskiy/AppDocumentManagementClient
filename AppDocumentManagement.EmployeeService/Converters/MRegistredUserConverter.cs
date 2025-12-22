using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeesService.Converters
{
    public class MRegistredUserConverter
    {
        public static RegistredUser ConvertToRegistredUser(MRegistredUser mRegistredUser)
        {
            RegistredUser registredUser = new RegistredUser();
            if (mRegistredUser != null)
            {
                registredUser.RegistredUserID = mRegistredUser.RegistredUserID;
                registredUser.RegistredUserLogin = mRegistredUser.RegistredUserLogin;
                if (mRegistredUser.RegistredUserPassword != "")
                {
                    registredUser.RegistredUserPassword = mRegistredUser.RegistredUserPassword;
                }
                registredUser.UserRole = UserRoleConverter.BackConvert(mRegistredUser.UserRole);
                if (mRegistredUser.RegistredUserTime != "")
                {
                    registredUser.UserRegistrationTime = DateTime.Parse(mRegistredUser.RegistredUserTime);
                }
                registredUser.EmployeeID = mRegistredUser.EmployeeID;
                registredUser.IsRegistered = mRegistredUser.IsRegistred;
            }
            return registredUser;
        }

        public static MRegistredUser ConvertToMRegistredUser(RegistredUser registredUser)
        {
            MRegistredUser mRegistredUser = new MRegistredUser();
            if (registredUser != null)
            {
                mRegistredUser.RegistredUserID = registredUser.RegistredUserID;
                mRegistredUser.RegistredUserLogin = registredUser.RegistredUserLogin;
                if (registredUser.RegistredUserPassword != null)
                {
                    mRegistredUser.RegistredUserPassword = registredUser.RegistredUserPassword;
                }
                mRegistredUser.UserRole = UserRoleConverter.ToIntConvert(registredUser.UserRole);
                if (registredUser.UserRegistrationTime != null)
                {
                    mRegistredUser.RegistredUserTime = registredUser.UserRegistrationTime.ToShortDateString();
                }
                mRegistredUser.EmployeeID = registredUser.EmployeeID;
                mRegistredUser.IsRegistred = registredUser.IsRegistered;
            }
            return mRegistredUser;
        }
    }
}

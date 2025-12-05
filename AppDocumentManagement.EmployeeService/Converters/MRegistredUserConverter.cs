using AppDocumentManagement.Models;

namespace AppDocumentManagement.EmployeeService.Converters
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
                registredUser.RegistredUserPassword = mRegistredUser.RegistredUserPassword;
                registredUser.UserRole = UserRoleConverter.BackConvert(mRegistredUser.UserRole);
                registredUser.RegistredUserTime = DateTime.Parse(mRegistredUser.RegistredUserTime);
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
                mRegistredUser.RegistredUserPassword = registredUser.RegistredUserPassword;
                mRegistredUser.UserRole = UserRoleConverter.ToIntConvert(registredUser.UserRole);
                mRegistredUser.RegistredUserTime = registredUser.RegistredUserTime.ToShortDateString();
                mRegistredUser.EmployeeID = registredUser.EmployeeID;
                mRegistredUser.IsRegistred = registredUser.IsRegistered;
            }
            return mRegistredUser;
        }
    }
}

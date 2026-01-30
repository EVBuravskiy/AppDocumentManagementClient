using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeesService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeesService.Services
{
    /// <summary>
    /// Class of service for sending and receiving Register User messages.
    /// </summary>
    public class RegisterUserService
    {
        /// <summary>
        /// Function for adding user registration data
        /// </summary>
        /// <param name="registredUser"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddRegistratedUser(RegistredUser registredUser)
        {
            MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(registredUser);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.AddRegistratedUser(mRegistredUser);
            return boolReply.Result;
        }
        /// <summary>
        /// Function to get all registered users
        /// </summary>
        /// <returns>List of RegistredUsers</returns>
        public async Task<List<RegistredUser>> GetAllRegistredUsers()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MRegistredUserList mRegistredUserList = client.GetAllRegistredUsers(new EmptyRequest());
            List<RegistredUser> registredUsers = new List<RegistredUser>();
            foreach (MRegistredUser mRegistredUser in mRegistredUserList.MRegistredUsers)
            {
                RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
                registredUsers.Add(registredUser);
            }
            return registredUsers;
        }
        /// <summary>
        /// Function for obtaining user data by employee ID number
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>RegistredUser</returns>
        public async Task<RegistredUser> GetRegistredUserByEmployeeID(int employeeID)
        {
            IDRequest iDRequest = new IDRequest() { ID = employeeID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MRegistredUser mRegistredUser = client.GetRegistredUserByEmployeeID(iDRequest);
            RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
            return registredUser;
        }
        /// <summary>
        /// User registration data update function
        /// </summary>
        /// <param name="registredUser"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateRegistratedUser(RegistredUser registredUser)
        {
            MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(registredUser);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.UpdateRegistratedUser(mRegistredUser);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for deleting user registration data
        /// </summary>
        /// <param name="registredUserID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveRegistratedUser(int registredUserID)
        {
            IDRequest iDRequest = new IDRequest() { ID = registredUserID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.RemoveRegistratedUser(iDRequest);
            return boolReply.Result;
        }
        /// <summary>
        /// Function for obtaining user data by login and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>RegistredUser</returns>
        public async Task<RegistredUser> GetRegistratedUser(string login, string password)
        {
            StringRequest stringRequest = new StringRequest() { Login = login, Password = password };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MRegistredUser mRegistredUser = client.GetRegistratedUser(stringRequest);
            RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
            return registredUser;
        }
        /// <summary>
        /// Checking administrator availability
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> CheckAviableAdministrator()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.CheckAviableAdministrator(new EmptyRequest());
            return boolReply.Result;
        }
    }
}

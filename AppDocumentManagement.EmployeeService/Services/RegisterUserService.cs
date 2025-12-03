using AppDocumentManagement.EmployeeService.Converters;
using AppDocumentManagement.EmployeeService.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeeService.Services
{
    public class RegisterUserService
    {
        public async Task<bool> AddRegistratedUser(RegistredUser registredUser)
        {
            MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(registredUser);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.AddRegistratedUser(mRegistredUser);
            return boolReply.Result;
        }

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

        public async Task<RegistredUser> GetRegistredUserByEmployeeID(int employeeID)
        {
            IDRequest iDRequest = new IDRequest() { ID = employeeID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MRegistredUser mRegistredUser = client.GetRegistredUserByEmployeeID(iDRequest);
            RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
            return registredUser;
        }

        public async Task<bool> UpdateRegistratedUser(RegistredUser registredUser)
        {
            MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(registredUser);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.UpdateRegistratedUser(mRegistredUser);
            return boolReply.Result;
        }

        public async Task<bool> RemoveRegistratedUser(int registredUserID)
        {
            IDRequest iDRequest = new IDRequest() { ID = registredUserID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.RemoveRegistratedUser(iDRequest);
            return boolReply.Result;
        }

        public async Task<RegistredUser> GetRegistratedUser(string login, string password)
        {
            StringRequest stringRequest = new StringRequest() { Login = login, Password = password };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MRegistredUser mRegistredUser = client.GetRegistratedUser(stringRequest);
            RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
            return registredUser;
        }

        public async Task<bool> CheckAviableAdministrator()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.CheckAviableAdministrator(new EmptyRequest());
            return boolReply.Result;
        }
    }
}

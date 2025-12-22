using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeesService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeesService.Service
{
    public class EmployesService
    {
        public async Task<bool> AddEmployee(Employee employee)
        {
            MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.AddEmployee(mEmployee);
            return boolReply.Result;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MEmployeesListReply mEmployeesList = client.GetAllEmployees(new EmptyRequest());
            List<Employee> employees = new List<Employee>();
            foreach (MEmployee mEmployee in mEmployeesList.Employees)
            {
                Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
                employees.Add(employee);
            }
            return employees;
        }

        public async Task<List<Employee>> GetAllAvailableEmployees()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MEmployeesListReply mEmployeesList = client.GetAllAvailableEmployees(new EmptyRequest());
            List<Employee> employees = new List<Employee>();
            foreach (MEmployee mEmployee in mEmployeesList.Employees)
            {
                Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
                employees.Add(employee);
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByID(int employeeID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest idRequest = new IDRequest() { ID = employeeID };
            MEmployee mEmployee = client.GetEmployeeByID(idRequest);
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesByDepartmentID(int departmentID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest idRequest = new IDRequest() { ID = departmentID };
            MEmployeesListReply mEmployeesList = client.GetEmployeesByDepartmentID(idRequest);
            List<Employee> employees = new List<Employee>();
            foreach (MEmployee mEmployee in mEmployeesList.Employees)
            {
                Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
                employees.Add(employee);
            }
            return employees;
        }

        public async Task<bool> RemoveEmployee(int employeeID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest idRequest = new IDRequest() { ID = employeeID };
            var boolReply = client.RemoveEmployee(idRequest);
            return boolReply.Result;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.UpdateEmployee(mEmployee);
            return boolReply.Result;
        }

        public async Task<Employee> GetGeneralDirector()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MEmployee mEmployee = client.GetGeneralDirector(new EmptyRequest());
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            return employee;
        }

        public async Task<bool> CheckForAviableEmployee()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.CheckForAviableEmployee(new EmptyRequest());
            return boolReply.Result;
        }
    }
}

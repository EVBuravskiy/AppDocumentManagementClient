using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeesService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeesService.Service
{
    /// <summary>
    /// Class of service for sending and receiving Employee messages.
    /// </summary>
    public class EmployesService
    {
        /// <summary>
        /// Add employee function
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// The function of obtaining all employees
        /// </summary>
        /// <returns>List of Employees</returns>
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
        /// <summary>
        /// Function to get a list of all available employees.
        /// </summary>
        /// <returns>List of Employees</returns>
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
        /// <summary>
        /// Function for obtaining an employee by ID number
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>Employee</returns>
        public async Task<Employee> GetEmployeeByID(int employeeID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest idRequest = new IDRequest() { ID = employeeID };
            MEmployee mEmployee = client.GetEmployeeByID(idRequest);
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            return employee;
        }
        /// <summary>
        /// Function for obtaining a list of employees by department identification number
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns>List of Employees</returns>
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
        /// <summary>
        /// Employee removal function
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveEmployee(int employeeID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest idRequest = new IDRequest() { ID = employeeID };
            var boolReply = client.RemoveEmployee(idRequest);
            return boolReply.Result;
        }
        /// <summary>
        /// Employee data update function
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.UpdateEmployee(mEmployee);
            return boolReply.Result;
        }
        /// <summary>
        /// CEO Data Retrieval Function
        /// </summary>
        /// <returns>Employee</returns>
        public async Task<Employee> GetGeneralDirector()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MEmployee mEmployee = client.GetGeneralDirector(new EmptyRequest());
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            return employee;
        }
        /// <summary>
        /// Employee availability check function
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> CheckForAviableEmployee()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.CheckForAviableEmployee(new EmptyRequest());
            return boolReply.Result;
        }
    }
}

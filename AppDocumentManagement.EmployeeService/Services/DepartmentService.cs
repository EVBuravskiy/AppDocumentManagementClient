using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeesService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeesService.Service
{
    public class DepartmentService
    {
        /// <summary>
        /// Function to add a new department
        /// </summary>
        /// <param name="department"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddDepartment(Department department)
        {
            MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.AddDepartment(mDepartment);
            return boolReply.Result;
        }
        /// <summary>
        /// Function to get all departments
        /// </summary>
        /// <returns>List of departments</returns>
        public async Task<List<Department>> GetAllDepartments()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            MDepartmentListReply mDepartmentList = client.GetAllDepartments(new EmptyRequest());
            List<Department> departmentList = new List<Department>();
            foreach (MDepartment mDepartment in mDepartmentList.MDepartments)
            {
                Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
                departmentList.Add(department);
            }
            return departmentList;
        }
        /// <summary>
        /// Function of obtaining department by ID number
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns>Department</returns>
        public async Task<Department> GetDepartmentByID(int departmentID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest iDRequest = new IDRequest() { ID = departmentID };
            MDepartment mDepartment = client.GetDepartmentByID(iDRequest);
            Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
            return department;
        }
        /// <summary>
        /// Department information update function
        /// </summary>
        /// <param name="department"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateDepartment(Department department)
        {
            MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.UpdateDepartment(mDepartment);
            return boolReply.Result;
        }
        /// <summary>
        /// Department delete function
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveDepartment(int departmentID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest iDRequest = new IDRequest() { ID = departmentID };
            var boolReply = client.RemoveDepartment(iDRequest);
            return boolReply.Result;
        }
    }
}

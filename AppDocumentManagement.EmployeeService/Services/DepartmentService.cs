using AppDocumentManagement.EmployeeService.Converters;
using AppDocumentManagement.EmployeeService.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeeService.Service
{
    public class DepartmentService
    {
        public async Task<bool> AddDepartment(Department department)
        {
            MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.AddDepartment(mDepartment);
            return boolReply.Result;
        }

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

        public async Task<Department> GetDepartmentByID(int departmentID)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            IDRequest iDRequest = new IDRequest() { ID = departmentID };
            MDepartment mDepartment = client.GetDepartmentByID(iDRequest);
            Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
            return department;
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.UpdateDepartment(mDepartment);
            return boolReply.Result;
        }

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

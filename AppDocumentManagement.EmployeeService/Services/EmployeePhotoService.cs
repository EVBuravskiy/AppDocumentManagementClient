using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeesService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeesService.Service
{
    /// <summary>
    /// Class of service for sending and receiving Employee photo messages
    /// </summary>
    public class EmployeePhotoService
    {
        /// <summary>
        /// Function for adding employee photo
        /// </summary>
        /// <param name="employeePhoto"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddEmployeePhoto(EmployeePhoto employeePhoto)
        {
            MEmployeePhoto mEmployeePhoto = MEmployeePhotoConverter.ConvertToMEmployeePhoto(employeePhoto);
            using var channel = GrpcChannel.ForAddress("http://localhost:6001", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024 
            });
            var client = new employeeApi.employeeApiClient(channel);
            var boolReply = client.AddEmployeePhoto(mEmployeePhoto);
            return boolReply.Result;
        }
        /// <summary>
        /// Function of receiving photos of all employees
        /// </summary>
        /// <returns>List of employee photos</returns>
        public async Task<List<EmployeePhoto>> GetEmployeePhotos()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:6001", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024 
            });
            var client = new employeeApi.employeeApiClient(channel);
            MEmployeePhotoList mEmployeePhotoList = client.GetEmployeePhotos(new EmptyRequest());
            List<EmployeePhoto> employeePhotos = new List<EmployeePhoto>();
            foreach(MEmployeePhoto mEmployeePhoto in mEmployeePhotoList.MEmployeePhotos)
            {
                EmployeePhoto employeePhoto = MEmployeePhotoConverter.ConvertToEmployeePhoto(mEmployeePhoto);
                employeePhotos.Add(employeePhoto);
            }
            return employeePhotos;
        }
        /// <summary>
        /// Function for obtaining a photo of an employee by his ID number
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public async Task<EmployeePhoto> GetEmployeePhotoByEmployeeID(int employeeID)
        {
            IDRequest iDRequest = new IDRequest() { ID = employeeID };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 20 * 1024 * 1024
            });
            var client = new employeeApi.employeeApiClient(channel);
            MEmployeePhoto mEmployeePhoto = client.GetEmployeePhotoByEmployeeID(iDRequest);
            if (mEmployeePhoto != null)
            {
                EmployeePhoto employeePhoto = MEmployeePhotoConverter.ConvertToEmployeePhoto(mEmployeePhoto);
                return employeePhoto;
            }
            return null;
        }
    }
}

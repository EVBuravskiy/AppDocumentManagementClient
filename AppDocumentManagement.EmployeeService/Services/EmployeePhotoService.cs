using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeesService.Converters;
using AppDocumentManagement.Models;
using Grpc.Net.Client;

namespace AppDocumentManagement.EmployeesService.Service
{
    public class EmployeePhotoService
    {
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

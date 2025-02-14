using Microsoft.AspNetCore.Http;
using HRMS.Shared.Utilities.Enums;
using HRMS.Shared.Utilities.Requests;

namespace HRMS.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
        Task<string> UploadAsync(IFormFile request,string fileName, UploadType uploadType);
    }
}

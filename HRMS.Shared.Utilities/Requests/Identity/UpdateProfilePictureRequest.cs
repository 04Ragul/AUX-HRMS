using Microsoft.AspNetCore.Http;

namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class UpdateProfilePictureRequest : UploadRequest
    {
        public int Id { get; set; }
    }

    public class UpdateStaffProfilePictureRequest 
    {
        public int Id { get; set; }
        public IFormFile formFile { get; set; }
    }
}

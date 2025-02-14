namespace HRMS.Shared.Utilities.Responses.Identity
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string location { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureDataUrl { get; set; }
        public string? DeviceId { get; set; }
    }
}

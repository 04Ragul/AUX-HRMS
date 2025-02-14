using System.ComponentModel.DataAnnotations;

namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class UpdateProfileRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }
        public string location { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

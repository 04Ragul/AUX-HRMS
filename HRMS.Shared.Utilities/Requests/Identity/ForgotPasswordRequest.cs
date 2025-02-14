using System.ComponentModel.DataAnnotations;

namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

      

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }
        public string Gender { get; set; }

        public bool ActivateUser { get; set; } = true;
        public bool AutoConfirmEmail { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class UpdatePasswordRequest
    {
        public int Id { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}

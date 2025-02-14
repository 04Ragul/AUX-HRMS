using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Utilities.Responses.Identity
{
    public class MobileTokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserImageURL { get; set; }
        public string DeviceId { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

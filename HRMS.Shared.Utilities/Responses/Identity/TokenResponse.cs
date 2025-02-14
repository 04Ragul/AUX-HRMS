using System.Security.Claims;

namespace HRMS.Shared.Utilities.Responses.Identity
{
    public class TokenResponse
    {
        public IEnumerable<Claim> Claims { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserImageURL { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

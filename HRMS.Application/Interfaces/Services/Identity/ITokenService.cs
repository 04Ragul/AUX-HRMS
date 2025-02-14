using HRMS.Shared.Utilities.Interfaces.Common;
using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Utilities.Responses.Identity;
using HRMS.Shared.Wrapper;

namespace HRMS.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);
        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}

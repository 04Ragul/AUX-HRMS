using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Utilities.Interfaces.Common;
using HRMS.Shared.Wrapper;

namespace HRMS.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);
        Task<IResult> UpdatePasswordAsync(UpdatePasswordRequest model);
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult<string>> GetProfilePictureAsync(int userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request);
        Task<IResult<string>> UpdateProfilePictureAsync(UpdateStaffProfilePictureRequest request);
    }
}

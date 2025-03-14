﻿using HRMS.Shared.Utilities.Interfaces.Common;
using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Utilities.Responses.Identity;
using HRMS.Shared.Wrapper;

namespace HRMS.Application.Interfaces.Services.Identity
{
    public interface IUserService : IService
    {
        Task<Result<List<UserResponse>>> GetAllAsync();
        Task<Result<int>> DeleteUser(int userId);
        Task<int> GetCountAsync();

        Task<IResult<UserResponse>> GetAsync(string userId);
        Task<IResult<UserResponse>> GetByNameAsync(string userName);
        Task<IResult<string>> ReleaseDevice(string userId);

        Task<IResult> RegisterAsync(RegisterRequest request, string origin);

        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

        Task<IResult<UserRolesResponse>> GetRolesAsync(string id);

        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);

        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

        Task<string> ExportToExcelAsync(string searchString = "");
    }
}

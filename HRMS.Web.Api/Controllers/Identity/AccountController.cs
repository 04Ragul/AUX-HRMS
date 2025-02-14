using HRMS.Application.Interfaces.Services;
using HRMS.Application.Interfaces.Services.Account;
using HRMS.Shared.Utilities.Requests.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Api.Controllers.Identity
{
    [Authorize]
    [Route("api/identity/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;       

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;            
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPut(nameof(UpdateProfile))]
        public async Task<ActionResult> UpdateProfile(UpdateProfileRequest model)
        {
            HRMS.Shared.Wrapper.IResult response = await _accountService.UpdateProfileAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPut(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest model)
        {
            HRMS.Shared.Wrapper.IResult response = await _accountService.ChangePasswordAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Get Profile picture by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("profile-picture/{userId}")]
        [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetProfilePictureAsync(string userId)
        {
            return Ok(await _accountService.GetProfilePictureAsync(int.Parse(userId)));
        }

        /// <summary>
        /// Update Profile Picture
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("profile-picture/{userId}")]
        public async Task<IActionResult> UpdateProfilePictureAsync(UpdateProfilePictureRequest request)
        {
            return Ok(await _accountService.UpdateProfilePictureAsync(request));
        }
    }
}

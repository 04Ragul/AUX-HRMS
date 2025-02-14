using HRMS.Application.Interfaces.Services;
using HRMS.Application.Interfaces.Services.Identity;
using HRMS.Shared.Utilities.Requests.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Api.Controllers.Identity
{
    [Route("api/identity/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _identityService;

        public TokenController(ITokenService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Get Token (Email, Password)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost]
        public async Task<ActionResult> Get(TokenRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HRMS.Shared.Wrapper.Result<HRMS.Shared.Utilities.Responses.Identity.TokenResponse> response = await _identityService.LoginAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest model)
        {
            HRMS.Shared.Wrapper.Result<HRMS.Shared.Utilities.Responses.Identity.TokenResponse> response = await _identityService.GetRefreshTokenAsync(model);
            return Ok(response);
        }
    }
}

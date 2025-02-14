using HRMS.Application.Interfaces.Services.Identity;
using HRMS.Shared.Utilities.Requests.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Shared.Constants.Permission;

namespace HRMS.Web.Api.Controllers.Identity
{
    [Route("api/identity/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get All Roles (basic, admin etc.)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            HRMS.Shared.Wrapper.Result<List<HRMS.Shared.Utilities.Responses.Identity.RoleResponse>> roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Add a Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(RoleRequest request)
        {
            HRMS.Shared.Wrapper.Result<string> response = await _roleService.SaveAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            HRMS.Shared.Wrapper.Result<string> response = await _roleService.DeleteAsync(int.Parse(id));
            return Ok(response);
        }

        /// <summary>
        /// Get Permissions By Role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.RoleClaims.View)]
        [HttpGet("permissions/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId([FromRoute] string roleId)
        {
            HRMS.Shared.Wrapper.Result<HRMS.Shared.Utilities.Responses.Identity.PermissionResponse> response = await _roleService.GetAllPermissionsAsync(int.Parse(roleId));
            return Ok(response);
        }

        /// <summary>
        /// Edit a Role Claim
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.RoleClaims.Edit)]
        [HttpPut("permissions/update")]
        public async Task<IActionResult> Update(PermissionRequest model)
        {
            HRMS.Shared.Wrapper.Result<string> response = await _roleService.UpdatePermissionsAsync(model);
            return Ok(response);
        }
    }
}

using HRMS.Shared.Utilities.Interfaces.Common;
using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Utilities.Responses.Identity;
using HRMS.Shared.Wrapper;

namespace HRMS.Application.Interfaces.Services.Identity
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(int id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(int id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(int roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}

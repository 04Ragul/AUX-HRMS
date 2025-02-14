using HRMS.Shared.Utilities.Interfaces.Common;
using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Utilities.Responses.Identity;
using HRMS.Shared.Wrapper;

namespace HRMS.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(int roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}

using HRMS.Shared.Utilities.Responses.Audit;
using HRMS.Shared.Wrapper;

namespace HRMS.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserProfileTrailsAsync(string userId);
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);
        Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}

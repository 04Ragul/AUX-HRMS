using HRMS.Shared.Utilities.Responses.Identity;

namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}

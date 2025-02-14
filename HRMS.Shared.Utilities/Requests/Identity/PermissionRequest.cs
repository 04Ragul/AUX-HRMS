namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class PermissionRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<RoleClaimRequest> RoleClaims { get; set; }
    }
}

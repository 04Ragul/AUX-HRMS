namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class ToggleUserStatusRequest
    {
        public bool ActivateUser { get; set; }
        public int UserId { get; set; }
    }
}

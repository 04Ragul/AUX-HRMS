namespace HRMS.Shared.Utilities.Responses.Identity
{
    public class ChatUserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePictureDataUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsOnline { get; set; }
    }
}

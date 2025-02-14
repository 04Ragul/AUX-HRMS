using HRMS.Domain.Interfaces.Chat;
namespace HRMS.Domain.Entities.Chat
{
    public partial class ChatHistory<TUser> : IChatHistory<TUser> where TUser : IChatUser
    {
        public long Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual TUser FromUser { get; set; }
        public virtual TUser ToUser { get; set; }
    }
}

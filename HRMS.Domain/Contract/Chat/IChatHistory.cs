﻿namespace HRMS.Domain.Interfaces.Chat
{
    public interface IChatHistory<TUser> where TUser : IChatUser
    {
        public long Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public TUser FromUser { get; set; }
        public TUser ToUser { get; set; }
    }
}

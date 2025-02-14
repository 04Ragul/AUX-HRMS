using AutoMapper;
using HRMS.Domain.Entities.Chat;
using HRMS.Domain.Interfaces.Chat;
using HRMS.Domain.Entities.Identity;
namespace HRMS.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            _ = CreateMap<ChatHistory<IChatUser>, ChatHistory<ApplicationUser>>().ReverseMap();
        }
    }
}

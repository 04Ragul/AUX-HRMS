using AutoMapper;
using HRMS.Domain.Entities.Identity;
using HRMS.Shared.Utilities.Responses.Identity;

namespace HRMS.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            _ = CreateMap<UserResponse, ApplicationUser>().ReverseMap();
            _ = CreateMap<ChatUserResponse, ApplicationUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}

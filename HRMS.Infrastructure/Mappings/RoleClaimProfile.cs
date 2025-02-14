using AutoMapper;
using HRMS.Domain.Entities.Identity;
using HRMS.Shared.Utilities.Requests.Identity;
using HRMS.Shared.Utilities.Responses.Identity;

namespace HRMS.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            _ = CreateMap<RoleClaimResponse, ApplicationRoleClaim>()
                .ForMember(nameof(ApplicationRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(ApplicationRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            _ = CreateMap<RoleClaimRequest, ApplicationRoleClaim>()
                .ForMember(nameof(ApplicationRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(ApplicationRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}

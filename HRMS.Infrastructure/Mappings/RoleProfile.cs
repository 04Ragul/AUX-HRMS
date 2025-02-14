using AutoMapper;
using HRMS.Domain.Entities.Identity;
using HRMS.Shared.Utilities.Responses.Identity;

namespace HRMS.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            _ = CreateMap<RoleResponse, ApplicationRole>().ReverseMap();
        }
    }
}

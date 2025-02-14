using AutoMapper;
using HRMS.Domain.Entities.Audit;
using HRMS.Shared.Utilities.Responses.Audit;

namespace HRMS.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            _ = CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}

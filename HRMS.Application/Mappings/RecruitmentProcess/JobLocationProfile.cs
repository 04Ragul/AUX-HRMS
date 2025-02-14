using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.AddEdit;
using HRMS.Domain.Entities.Features.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Mappings.RecruitmentProcess
{
    public class JobLocationProfile : Profile
    {
        public JobLocationProfile()
        {
            _ = CreateMap<AddEditJobLocationCommand, JobLocation>().ReverseMap();
        }
    }
}

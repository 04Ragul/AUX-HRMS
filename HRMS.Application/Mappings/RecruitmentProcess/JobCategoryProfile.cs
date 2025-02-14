using AutoMapper;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Domain.Entities.Features.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Mappings.RecruitmentProcess
{
    internal class JobCategoryProfile : Profile
    {
        public JobCategoryProfile()
        {
            _ = CreateMap<AddEditJobCategoryCommand, JobCategory>().ReverseMap();
        }
    }
}

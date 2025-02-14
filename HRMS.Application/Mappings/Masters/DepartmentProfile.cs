using AutoMapper;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.Features.Employees.Commands.AddEdit;
using HRMS.Domain.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Mappings
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            _ = CreateMap<AddEditDeparmentCommand, Department>().ReverseMap();
        }
    }
}

using AutoMapper;
using HRMS.Application.Features.Employees.Commands.AddEdit;
using HRMS.Domain.Entities.Features;
using HRMS.Domain.Entities.Identity;
using HRMS.Shared.Utilities.Requests.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            _ = CreateMap<AddEditEmployeeCommand, Employee>().ReverseMap();
            _ = CreateMap<AddEditEmployeeCommand, RegisterRequest>().ReverseMap();
        }
    }
}

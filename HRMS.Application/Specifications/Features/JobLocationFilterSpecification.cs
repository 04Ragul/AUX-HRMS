using HRMS.Application.Specifications.Base;
using HRMS.Domain.Entities.Features.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Specifications.Features
{
    internal class JobLocationFilterSpecification : Specification<JobLocation>
    {
        public JobLocationFilterSpecification(string searchString)
        {
            Criteria = !string.IsNullOrEmpty(searchString)
                ? (p => p.Country.Contains(searchString) || p.State.Contains(searchString) || p.City.Contains(searchString) || p.Address.Contains(searchString))
                : (p => true);
        }
    }
}

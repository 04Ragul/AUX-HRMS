using HRMS.Application.Specifications.Base;
using HRMS.Domain.Entities.Features.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Specifications.Features
{
    internal class RoundFilterSpecification : Specification<Round>
    {
        public RoundFilterSpecification(string searchString)
        {
            Criteria = !string.IsNullOrEmpty(searchString)
                ? (p => p.Name.Contains(searchString))
                : (p => true);
        }
    }
}

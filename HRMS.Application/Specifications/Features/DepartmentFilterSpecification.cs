using HRMS.Application.Specifications.Base;
using HRMS.Domain.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Specifications.Features
{
    internal class DepartmentFilterSpecification : Specification<Department>
    {
        public DepartmentFilterSpecification(string searchString)
        {
            Criteria = !string.IsNullOrEmpty(searchString)
                ? (p => p.Name.Contains(searchString) || p.Code.Contains(searchString))
                : (p => true);
        }
    }
}

using HRMS.Application.Specifications.Base;
using HRMS.Domain.Entities.Features.Organisations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Specifications.Features
{
    internal class LocationFilterSpecification : Specification<Location>
    {
        public LocationFilterSpecification(string searchString)
        {
            Criteria = !string.IsNullOrEmpty(searchString)
                ? (p => p.Name.Contains(searchString) || p.Latitude.Contains(searchString) || p.Longitude.Contains(searchString))
                : (p => true);
        }
    }
}

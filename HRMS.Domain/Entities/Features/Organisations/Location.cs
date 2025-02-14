using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Organisations
{
    public class Location : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

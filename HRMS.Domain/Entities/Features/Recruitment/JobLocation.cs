using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class JobLocation : AuditableEntity<int>
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
    }
}

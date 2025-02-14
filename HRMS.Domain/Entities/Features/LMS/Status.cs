using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.LMS
{
    public class Status : AuditableEntity<int>
    {
        public string StatusName { get; set; }
        public string Description { get; set; }
    }
}

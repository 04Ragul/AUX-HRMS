using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features
{
    public class Department : AuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

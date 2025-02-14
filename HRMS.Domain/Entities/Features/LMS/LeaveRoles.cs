using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.LMS
{
    public class LeaveRoles : AuditableEntity<int>
    {
        public string name { get; set; }
        public string description { get; set; }
        public int LeaveId { get; set; }
        public string MinSalary { get; set; }
        public string MaxSalary { get; set; }
        public int PaidLeaves { get; set; }
    }
}

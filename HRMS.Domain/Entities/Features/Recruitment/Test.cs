using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class Test : AuditableEntity<int>
    {
        public string TestName { get; set; }
        public string TestSummary { get; set; }
        public string TestType { get; set; }
        public int NoOfQue {  get; set; }
        public DateTime TestTime { get; set; }
    }
}

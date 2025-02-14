using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class TestStatus : AuditableEntity<int>
    {
        public int TestId { get; set; }
        public int ApplicantId {  get; set; }
        public DateTime TestDuration { get; set; }
        public int TotalScore { get; set; }
        public int TestScore { get; set; }
        public string Result { get; set; }
    }
}

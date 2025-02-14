using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class ApplicantTest : AuditableEntity<int>
    {
        public int ApplicantId { get; set; }
        public int TestId { get; set; }
        public DateTime AppearedOn { get; set; }
    }
}

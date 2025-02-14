using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class Process : AuditableEntity<int>
    {
        public int ApplicantTestId { get; set; }
        public int RoundId { get; set; }
    }
}

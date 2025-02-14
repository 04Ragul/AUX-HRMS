using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class ApplicationHistory : AuditableEntity<int>
    {
        public int RoundId { get; set; }
        public int ProcessId { get; set; }
        public int ApplicantId { get; set; }
        public int JobId { get; set; }
        public int ApplicationId { get; set; }

    }
}

using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class ApplicantScreening : AuditableEntity<int>
    {
        public int ApplicantId { get; set; }
        public int RecruiterId { get; set; }
        public string InterviewType { get; set; }
        public string InterviewVenue { get; set; }
    }
}

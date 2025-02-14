using HRMS.Domain.Contract;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class Applicant : AuditableEntity<int>
    {   
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Contact { get; set; }
        public string ProfessionalSummary { get; set; }
        public string HighestEducation { get; set; }
        public string ProfileImage { get; set; }
        public string Resume { get; set; }
        public GenderType Gender { get; set; }

    }
}

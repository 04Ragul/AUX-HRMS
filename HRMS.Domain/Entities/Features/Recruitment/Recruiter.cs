using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class Recruiter : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public string Mobile {  get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}

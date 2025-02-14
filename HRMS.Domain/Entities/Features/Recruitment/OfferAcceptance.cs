using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class OfferAcceptance : AuditableEntity<int>
    {
        public int ApplicantId { get; set; }
        public int CompanyId { get; set; }
        public int JobId { get; set; }
        public bool OfferApproved { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Package { get; set; }
    }
}

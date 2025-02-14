using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.LMS
{
    public class State : AuditableEntity<int>
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string StateDescription { get; set; }
        public int CountryId { get; set; }
        public int StatusId { get; set; }
    }
}

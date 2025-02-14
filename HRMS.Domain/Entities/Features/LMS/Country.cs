using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.LMS
{
    public class Country : AuditableEntity<int>
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public int StatusId { get; set; }
    }
}

using HRMS.Domain.Contract;
using HRMS.Domain.Entities.Features.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.LMS
{
    public class Holiday : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int WDay { get; set; }
        public DateTime Date { get; set; }
        public int RegionId { get; set; }

        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }


    }
}

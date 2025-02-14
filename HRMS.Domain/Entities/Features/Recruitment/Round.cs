using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class Round:AuditableEntity<int>
    {
        public string Name { get; set; }
        public int JobCategoryId { get; set; }
    }
}

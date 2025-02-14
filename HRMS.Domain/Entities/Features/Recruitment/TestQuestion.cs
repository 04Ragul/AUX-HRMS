using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Recruitment
{
    public class TestQuestion : AuditableEntity<int>
    {
        public int TestId { get; set; }
        public int OptionId { get; set; }
        public string Question { get; set; }
    }
}

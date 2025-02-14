using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Masters
{
    public class Designation : AuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

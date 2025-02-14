using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Employees
{
    public class EmployeeHirachyMapping : AuditableEntity<int>
    {
        public int EmployeeId { get; set; }
        public int ReportingToId { get; set; }
    }
}

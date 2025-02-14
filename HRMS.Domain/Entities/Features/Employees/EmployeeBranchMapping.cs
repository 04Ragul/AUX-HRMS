using HRMS.Domain.Contract;
using HRMS.Domain.Entities.Features.Organisations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Employees
{
    public class EmployeeBranchMapping : AuditableEntity<int>
    {
        public int EmployeeId { get; set; }
        public int BranchId { get; set; }
    }
}

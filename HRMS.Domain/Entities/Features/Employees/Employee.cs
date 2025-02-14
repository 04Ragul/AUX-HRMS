using HRMS.Domain.Contract;
using HRMS.Domain.Entities.Features.Employees;
using HRMS.Domain.Entities.Features.Masters;
using HRMS.Domain.Entities.Features.Organisations;
using HRMS.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features
{
    public class Employee : AuditableEntity<int>
    {
        public object Position;
        public string Name;

        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime DoB { get; set; }
        public DateTime ReleavingDate { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public object Email { get; set; }
    }
}

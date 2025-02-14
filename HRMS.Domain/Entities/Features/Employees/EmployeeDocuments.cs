using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Contract;
using HRMS.Domain.Entities.Features.Masters;

namespace HRMS.Domain.Entities.Features.Employees
{
    public class EmployeeDocuments : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocPath { get; set; }
        public string MimeType { get; set; }
        public string DocName { get; set; }
        public int EmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
    }
}

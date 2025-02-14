using HRMS.Domain.Contract;
using HRMS.Domain.Entities.Features.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Organisations
{
    public class Branch : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Mobile {  get; set; }
        public string? LandLine { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int LocationId { get; set; }
        public int OrganisationId { get; set; }
       
    }
}

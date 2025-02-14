using HRMS.Domain.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Organisations
{
    public class Organisation : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Landline { get; set; }
        public string MobileNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string State {  get; set; }
        public string City { get; set; }
        public string Logo { get; set; }
     }
}

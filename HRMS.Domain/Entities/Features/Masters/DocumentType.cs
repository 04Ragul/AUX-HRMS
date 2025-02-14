using HRMS.Domain.Contract;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities.Features.Masters
{
    public class DocumentType : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProofType ProofType { get; set; }
    }
}

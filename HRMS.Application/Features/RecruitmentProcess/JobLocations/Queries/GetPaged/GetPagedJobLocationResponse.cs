using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobLocations.Queries.GetPaged
{
    public class GetPagedJobLocationResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}

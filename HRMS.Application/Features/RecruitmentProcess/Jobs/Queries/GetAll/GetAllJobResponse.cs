using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetAll
{
    public class GetAllJobResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Salary { get; set; }
        public string NoOfVacancy { get; set; }
        public string Status { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public int JobCategoryId { get; set; }
        public int JobLocationId { get; set; }
        public int CompanyId { get; set; }
    }
}

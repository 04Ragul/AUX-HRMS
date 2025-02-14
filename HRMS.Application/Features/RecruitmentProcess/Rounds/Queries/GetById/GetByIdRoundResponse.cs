using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetById
{
    public class GetByIdRoundResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobCategoryId { get; set; }
    }
}

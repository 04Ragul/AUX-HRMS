﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetPaginated
{
    public class GetPaginatedRoundResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobCategoryId { get; set; }
    }
}

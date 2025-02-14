using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Tests.Commands.AddEdit
{
    public class AddEditTestCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string TestSummary { get; set; }
        public string TestType { get; set; }
        public int NoOfQue { get; set; }
        public DateTime TestTime { get; set; }
    }
}

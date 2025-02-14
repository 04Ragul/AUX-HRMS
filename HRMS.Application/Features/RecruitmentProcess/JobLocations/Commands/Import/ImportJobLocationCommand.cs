using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.Import
{
    public class ImportJobLocationCommand : IRequest<Result<int>>
    {
    }
}

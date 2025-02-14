using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.JobApplicatResults.Queries.GetById;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicatTests.Queries.GetById
{
    public class GetByIdJobApplicationTestQuery : IRequest<Result<GetByIdJobApplicationTestResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobApplicationTestQueryHandler : IRequestHandler<GetByIdJobApplicationTestQuery, Result<GetByIdJobApplicationTestResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobApplicationTestQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobApplicationTestQueryHandler> _logger;

        public GetByIdJobApplicationTestQueryHandler(IStringLocalizer<GetByIdJobApplicationTestQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobApplicationTestQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobApplicationTestResponse>> Handle(GetByIdJobApplicationTestQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

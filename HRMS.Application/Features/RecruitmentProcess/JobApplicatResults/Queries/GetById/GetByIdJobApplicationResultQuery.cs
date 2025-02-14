using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.JobApplicationScreening.Queries.GetById;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicatResults.Queries.GetById
{
    public class GetByIdJobApplicationResultQuery : IRequest<Result<GetByIdJobApplicationResultResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobApplicationResultQueryHandler : IRequestHandler<GetByIdJobApplicationResultQuery, Result<GetByIdJobApplicationResultResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobApplicationResultQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobApplicationResultQueryHandler> _logger;

        public GetByIdJobApplicationResultQueryHandler(IStringLocalizer<GetByIdJobApplicationResultQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobApplicationResultQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobApplicationResultResponse>> Handle(GetByIdJobApplicationResultQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

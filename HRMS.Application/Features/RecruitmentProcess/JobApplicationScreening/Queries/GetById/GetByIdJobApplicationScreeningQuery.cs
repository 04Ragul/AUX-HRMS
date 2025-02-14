using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.JobApplication.Queries.GetById;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicationScreening.Queries.GetById
{
    public class GetByIdJobApplicationScreeningQuery : IRequest<Result<GetByIdJobApplicationScreeningResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobApplicationScreeningQueryHandler : IRequestHandler<GetByIdJobApplicationScreeningQuery, Result<GetByIdJobApplicationScreeningResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobApplicationScreeningQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobApplicationScreeningQueryHandler> _logger;

        public GetByIdJobApplicationScreeningQueryHandler(IStringLocalizer<GetByIdJobApplicationScreeningQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobApplicationScreeningQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobApplicationScreeningResponse>> Handle(GetByIdJobApplicationScreeningQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

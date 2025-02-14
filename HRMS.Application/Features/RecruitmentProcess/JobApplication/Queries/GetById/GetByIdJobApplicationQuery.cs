using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetById;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobApplication.Queries.GetById
{
    public class GetByIdJobApplicationQuery : IRequest<Result<GetByIdJobApplicationResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobApplicationQueryHandler : IRequestHandler<GetByIdJobApplicationQuery, Result<GetByIdJobApplicationResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobApplicationQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobApplicationQueryHandler> _logger;

        public GetByIdJobApplicationQueryHandler(IStringLocalizer<GetByIdJobApplicationQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobApplicationQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobApplicationResponse>> Handle(GetByIdJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

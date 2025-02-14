using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Queries.GetById;
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

namespace HRMS.Application.Features.RecruitmentProcess.Processes.Queries.GetById
{
    public class GetByIdProcessQuery : IRequest<Result<GetByIdProcessResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdProcessQueryHandler : IRequestHandler<GetByIdProcessQuery, Result<GetByIdProcessResponse>>
    {
        private readonly IStringLocalizer<GetByIdProcessQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdProcessQueryHandler> _logger;

        public GetByIdProcessQueryHandler(IStringLocalizer<GetByIdProcessQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdProcessQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdProcessResponse>> Handle(GetByIdProcessQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

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

namespace HRMS.Application.Features.RecruitmentProcess.JobLocations.Queries.GetById
{
    public class GetByIdJobLocationQuery : IRequest<Result<GetByIdJobLocationResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobLocationQueryHandler : IRequestHandler<GetByIdJobLocationQuery, Result<GetByIdJobLocationResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobLocationQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobLocationQueryHandler> _logger;

        public GetByIdJobLocationQueryHandler(IStringLocalizer<GetByIdJobLocationQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobLocationQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobLocationResponse>> Handle(GetByIdJobLocationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

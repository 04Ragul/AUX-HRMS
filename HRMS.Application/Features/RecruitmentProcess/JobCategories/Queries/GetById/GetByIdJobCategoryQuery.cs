using AutoMapper;
using HRMS.Application.Features.RecruitmentProcess.JobApplicatTests.Queries.GetById;
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

namespace HRMS.Application.Features.RecruitmentProcess.JobCategories.Queries.GetById
{
    public class GetByIdJobCategoryQuery : IRequest<Result<GetByIdJobCategoryResponse>>
    {
        public int Id { get; set; }
    }
    internal class GetByIdJobCategoryQueryHandler : IRequestHandler<GetByIdJobCategoryQuery, Result<GetByIdJobCategoryResponse>>
    {
        private readonly IStringLocalizer<GetByIdJobCategoryQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdJobCategoryQueryHandler> _logger;

        public GetByIdJobCategoryQueryHandler(IStringLocalizer<GetByIdJobCategoryQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetByIdJobCategoryQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetByIdJobCategoryResponse>> Handle(GetByIdJobCategoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using HRMS.Application.Extensions;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Queries.GetPaged;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Specifications.Features;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobCategories.Queries.GetPaged
{
    public class GetPaginatedJobCategoryQuery:IRequest<PaginatedResult<GetPaginatedJobCategoryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedJobCategoryQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }
    internal class GetPaginatedJobCategoryQueryHandler : IRequestHandler<GetPaginatedJobCategoryQuery, PaginatedResult<GetPaginatedJobCategoryResponse>>
    {
        private readonly IStringLocalizer<GetPaginatedJobCategoryQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedJobCategoryQueryHandler> _logger;

        public GetPaginatedJobCategoryQueryHandler(IStringLocalizer<GetPaginatedJobCategoryQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetPaginatedJobCategoryQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedResult<GetPaginatedJobCategoryResponse>> Handle(GetPaginatedJobCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Recruitment.JobCategory, GetPaginatedJobCategoryResponse>> expression = e => new GetPaginatedJobCategoryResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                };
                JobCategoryFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetPaginatedJobCategoryResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.JobCategory>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetPaginatedJobCategoryResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.JobCategory>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetPaginatedJobCategoryResponse>)await PaginatedResult<GetPaginatedJobCategoryResponse>.FailAsync(ex.Message);
            }
        }
    }
}

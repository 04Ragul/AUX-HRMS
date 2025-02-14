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

namespace HRMS.Application.Features.RecruitmentProcess.JobLocations.Queries.GetPaged
{
    public class GetPagedJobLocationQuery : IRequest<PaginatedResult<GetPagedJobLocationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPagedJobLocationQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPagedJobLocationQueryHandler : IRequestHandler<GetPagedJobLocationQuery, PaginatedResult<GetPagedJobLocationResponse>>
    {
        private readonly IStringLocalizer<GetPagedJobLocationQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPagedJobLocationQueryHandler> _logger;

        public GetPagedJobLocationQueryHandler(IStringLocalizer<GetPagedJobLocationQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetPagedJobLocationQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedResult<GetPagedJobLocationResponse>> Handle(GetPagedJobLocationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Recruitment.JobLocation, GetPagedJobLocationResponse>> expression = e => new GetPagedJobLocationResponse
                {
                    Id = e.Id,
                    Address = e.Address,
                    City = e.City,
                    Country = e.Country,
                    CreatedOn = e.CreatedOn,
                    State = e.State
                };
                JobLocationFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetPagedJobLocationResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.JobLocation>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetPagedJobLocationResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.JobLocation>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetPagedJobLocationResponse>)await PaginatedResult<GetPagedJobLocationResponse>.FailAsync(ex.Message);
            }
        }
    }
}

using AutoMapper;
using HRMS.Application.Extensions;
using HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetPaged;
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

namespace HRMS.Application.Features.RecruitmentProcess.Jobs.Queries.GetPaged
{
    public class GetPagedJobQuery : IRequest<PaginatedResult<GetPagedJobResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[]? OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPagedJobQuery(int pageNumber, int pageSize, string searchString, string? orderBy)
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
    internal class GetPagedJobQueryHandler : IRequestHandler<GetPagedJobQuery, PaginatedResult<GetPagedJobResponse>>
    {
        private readonly IStringLocalizer<GetPagedJobQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPagedJobQueryHandler> _logger;

        public GetPagedJobQueryHandler(IStringLocalizer<GetPagedJobQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetPagedJobQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedResult<GetPagedJobResponse>> Handle(GetPagedJobQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Recruitment.Job, GetPagedJobResponse>> expression = e => new GetPagedJobResponse
                {
                    Id = e.Id,
                    Title = e.Title,
                    CompanyId = e.CompanyId,
                    Description = e.Description,
                    JobLocationId = e.JobLocationId,
                    JobCategoryId = e.JobCategoryId,
                    LastApplicationDate = e.LastApplicationDate,
                    NoOfVacancy = e.NoOfVacancy,
                    Salary = e.Salary,
                    Status = e.Status,
                    Type = e.Type
                };
                JobFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetPagedJobResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Job>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetPagedJobResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Job>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetPagedJobResponse>)await PaginatedResult<GetPagedJobResponse>.FailAsync(ex.Message);
            }
        }
    }
}

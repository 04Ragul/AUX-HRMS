using AutoMapper;
using HRMS.Application.Extensions;
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

namespace HRMS.Application.Features.RecruitmentProcess.Rounds.Queries.GetPaginated
{
    public class GetPaginatedRoundQuery : IRequest<PaginatedResult<GetPaginatedRoundResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[]? OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedRoundQuery(int pageNumber, int pageSize, string searchString, string? orderBy)
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
    internal class GetPaginatedRoundQueryHandler : IRequestHandler<GetPaginatedRoundQuery, PaginatedResult<GetPaginatedRoundResponse>>
    {
        private readonly IStringLocalizer<GetPaginatedRoundQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedRoundQueryHandler> _logger;

        public GetPaginatedRoundQueryHandler(IStringLocalizer<GetPaginatedRoundQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetPaginatedRoundQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedResult<GetPaginatedRoundResponse>> Handle(GetPaginatedRoundQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Recruitment.Round, GetPaginatedRoundResponse>> expression = e => new GetPaginatedRoundResponse
                {
                    Id = e.Id,
                    JobCategoryId = e.JobCategoryId,
                    Name = e.Name
                };
                RoundFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetPaginatedRoundResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Round>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetPaginatedRoundResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Recruitment.Round>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetPaginatedRoundResponse>)await PaginatedResult<GetPaginatedRoundResponse>.FailAsync(ex.Message);
            }
        }
    }
}

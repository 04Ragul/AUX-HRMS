using AutoMapper;
using HRMS.Application.Extensions;
using HRMS.Application.Features.Departments.Commands.Delete;
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

namespace HRMS.Application.Features.Departments.Queries.GetPaginated
{
    public class GetDepartmentPaginatedQuery : IRequest<PaginatedResult<GetDepartmentPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[]? OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetDepartmentPaginatedQuery(int pageNumber, int pageSize, string searchString, string? orderBy)
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
    internal class GetDepartmentPaginatedQueryHandler : IRequestHandler<GetDepartmentPaginatedQuery, PaginatedResult<GetDepartmentPaginatedResponse>>
    {
        private readonly IStringLocalizer<GetDepartmentPaginatedQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDepartmentPaginatedQueryHandler> _logger;

        public GetDepartmentPaginatedQueryHandler(IStringLocalizer<GetDepartmentPaginatedQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetDepartmentPaginatedQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<GetDepartmentPaginatedResponse>> Handle(GetDepartmentPaginatedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Department, GetDepartmentPaginatedResponse>> expression = e => new GetDepartmentPaginatedResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Code = e.Code,
                };
                DepartmentFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetDepartmentPaginatedResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Department>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetDepartmentPaginatedResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Department>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetDepartmentPaginatedResponse>)await PaginatedResult<GetDepartmentPaginatedResponse>.FailAsync(ex.Message);
            }
        }
    }
}

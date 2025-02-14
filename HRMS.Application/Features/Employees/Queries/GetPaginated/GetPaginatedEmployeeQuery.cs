using AutoMapper;
using HRMS.Application.Extensions;
using HRMS.Application.Features.Departments.Queries.GetPaginated;
using HRMS.Application.Features.Employees.Commands.Delete;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Specifications.Features;
using HRMS.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Employees.Queries.GetPaginated
{
    public class GetEmployeePaginatedQuery : IRequest<PaginatedResult<GetEmployeePaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[]? OrderBy { get; set; }

        public GetEmployeePaginatedQuery(int pageNumber, int pageSize, string searchString, string? orderBy)
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
    public class GetPaginatedEmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; } // Added department field
    }

    internal class GetEmployeePaginatedQueryHandler : IRequestHandler<GetEmployeePaginatedQuery, PaginatedResult<GetEmployeePaginatedResponse>>
    {
        private readonly IStringLocalizer<GetEmployeePaginatedQueryHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeePaginatedQueryHandler> _logger;

        public GetEmployeePaginatedQueryHandler(IStringLocalizer<GetEmployeePaginatedQueryHandler> localizer, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetEmployeePaginatedQueryHandler> logger)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<GetEmployeePaginatedResponse>> Handle(GetEmployeePaginatedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Employee, GetEmployeePaginatedResponse>> expression = static e => new GetEmployeePaginatedResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Position = e.Position,
                };
                DepartmentFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetEmployeePaginatedResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Employee>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetDepartmentPaginatedResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Employee>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetEmployeePaginatedResponse>)await PaginatedResult<GetEmployeePaginatedResponse>.FailAsync(ex.Message);
            }
        }
    }
}
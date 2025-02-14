using AutoMapper;
using HRMS.Application.Extensions;
using HRMS.Application.Features.Departments.Queries.GetPaginated;
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

namespace HRMS.Application.Features.Organisations.Locations.Queries.GetPaginated
{
    public class GetPaginatedOrganisationLocationQuery : IRequest<PaginatedResult<GetPaginatedOrganisationLocationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedOrganisationLocationQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedOrganisationLocationQueryHandler : IRequestHandler<GetPaginatedOrganisationLocationQuery, PaginatedResult<GetPaginatedOrganisationLocationResponse>>
    {
        private readonly IStringLocalizer<GetPaginatedOrganisationLocationQueryHandler> _localize;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedOrganisationLocationQueryHandler> _logger;

        public GetPaginatedOrganisationLocationQueryHandler(IStringLocalizer<GetPaginatedOrganisationLocationQueryHandler> localize, IUnitOfWork<int> unitOfWork, IMapper mapper, ILogger<GetPaginatedOrganisationLocationQueryHandler> logger)
        {
            _localize = localize;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<GetPaginatedOrganisationLocationResponse>> Handle(GetPaginatedOrganisationLocationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Domain.Entities.Features.Organisations.Location, GetPaginatedOrganisationLocationResponse>> expression = e => new GetPaginatedOrganisationLocationResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Longitude = e.Longitude,
                    Latitude = e.Latitude,
                    CreatedOn = e.CreatedOn!.Value,
                };
                LocationFilterSpecification ManualFilterSpec = new(request.SearchString);
                if (request.OrderBy?.Any() != true)
                {
                    PaginatedResult<GetPaginatedOrganisationLocationResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Organisations.Location>().Entities
                       .Specify(ManualFilterSpec)
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;
                }
                else
                {
                    string ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                    PaginatedResult<GetPaginatedOrganisationLocationResponse> data = await _unitOfWork.Repository<Domain.Entities.Features.Organisations.Location>().Entities
                       .Specify(ManualFilterSpec)
                       .OrderBy(ordering) // require system.linq.dynamic.core
                       .Select(expression)
                       .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                    return data;

                }
            }
            catch (Exception ex)
            {
                return (PaginatedResult<GetPaginatedOrganisationLocationResponse>)await PaginatedResult<GetPaginatedOrganisationLocationResponse>.FailAsync(ex.Message);
            }
        }
    }
}

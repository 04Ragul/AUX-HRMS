
using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Companies.Queries.GetPaginated
{
    public class GetPaginatedOrganisationQuery : IRequest<PaginatedResult<GetPaginatedOrganisationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...
        public GetPaginatedOrganisationQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedOrganisationQueryHandler : IRequestHandler<GetPaginatedOrganisationQuery, PaginatedResult<GetPaginatedOrganisationResponse>>
    {
        public async Task<PaginatedResult<GetPaginatedOrganisationResponse>> Handle(GetPaginatedOrganisationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

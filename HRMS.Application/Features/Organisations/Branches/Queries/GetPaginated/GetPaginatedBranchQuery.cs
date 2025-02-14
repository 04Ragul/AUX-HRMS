using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Branches.Queries.GetPaginated
{
    public class GetPaginatedBranchQuery:IRequest<PaginatedResult<GetPaginatedBranchResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedBranchQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedBranchQueryHandler : IRequestHandler<GetPaginatedBranchQuery, PaginatedResult<GetPaginatedBranchResponse>>
    {
        public async Task<PaginatedResult<GetPaginatedBranchResponse>> Handle(GetPaginatedBranchQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

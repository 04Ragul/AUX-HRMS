using HRMS.Application.Features.Organisations.Branches.Queries.GetPaginated;
using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Tests.Queries.GetPaginated
{
    public class GetPaginatedTestQuery:IRequest<PaginatedResult<GetPaginatedTestResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...
        public GetPaginatedTestQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedTestQueryHandler : IRequestHandler<GetPaginatedTestQuery, PaginatedResult<GetPaginatedTestResponse>>
    {
        public async Task<PaginatedResult<GetPaginatedTestResponse>> Handle(GetPaginatedTestQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

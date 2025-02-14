
using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.Processes.Queries.GetPaginated
{
    public class GetPaginatedProcessQuery : IRequest<PaginatedResult<GetPaginatedProcessResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedProcessQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedProcessQueryHandler : IRequestHandler<GetPaginatedProcessQuery, PaginatedResult<GetPaginatedProcessResponse>>
    {
        public async Task<PaginatedResult<GetPaginatedProcessResponse>> Handle(GetPaginatedProcessQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

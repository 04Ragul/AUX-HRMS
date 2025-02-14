
using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicatResults.Queries.GetPaginated
{
    public class GetPaginatedJobApplicationresultQuery:IRequest<PaginatedResult<GetPaginatedJobApplicationResultResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedJobApplicationresultQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedJobApplicationresultQueryHandler : IRequestHandler<GetPaginatedJobApplicationresultQuery, PaginatedResult<GetPaginatedJobApplicationResultResponse>>
    {
        public async Task<PaginatedResult<GetPaginatedJobApplicationResultResponse>> Handle(GetPaginatedJobApplicationresultQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

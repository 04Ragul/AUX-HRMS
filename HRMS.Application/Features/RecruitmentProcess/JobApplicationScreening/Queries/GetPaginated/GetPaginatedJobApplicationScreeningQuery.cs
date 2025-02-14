
using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.RecruitmentProcess.JobApplicationScreening.Queries.GetPaginated
{
    public class GetPaginatedJobApplicationScreeningQuery:IRequest<PaginatedResult<GetPaginatedJobApplicationScreeningResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetPaginatedJobApplicationScreeningQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
    internal class GetPaginatedJobApplicationScreeningQueryHandler : IRequestHandler<GetPaginatedJobApplicationScreeningQuery, PaginatedResult<GetPaginatedJobApplicationScreeningResponse>>
    {
        public async Task<PaginatedResult<GetPaginatedJobApplicationScreeningResponse>> Handle(GetPaginatedJobApplicationScreeningQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

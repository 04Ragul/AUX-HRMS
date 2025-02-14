using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Locations.Queries.GetPaginated
{
    public class GetPaginatedOrganisationLocationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

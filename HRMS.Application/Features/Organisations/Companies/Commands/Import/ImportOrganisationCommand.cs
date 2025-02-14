using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Companies.Commands.Import
{
    public class ImportOrganisationCommand:IRequest<Result<int>>
    {
        public string Data { get; set; }
    }
    internal class ImportOrganisationCommandHandler : IRequestHandler<ImportOrganisationCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(ImportOrganisationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

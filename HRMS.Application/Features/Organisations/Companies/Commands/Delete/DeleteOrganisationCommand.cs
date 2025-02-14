using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Companies.Commands.Delete
{
    public class DeleteOrganisationCommand:IRequest<Result<int>>
    {
        public int Id { get; set; } 
    }
    internal class DeleteOrganisationCommandHandler : IRequestHandler<DeleteOrganisationCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using HRMS.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Organisations.Companies.Commands.AddEdit
{
    public class AddEditOrganisationCommand:IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Landline { get; set; }
        public string MobileNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Logo { get; set; }
    }
    internal class AddEditOrganisationCommandHandler : IRequestHandler<AddEditOrganisationCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddEditOrganisationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

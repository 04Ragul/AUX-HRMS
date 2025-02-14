using FluentValidation;
using HRMS.Application.Features.Organisations.Locations.Commands.AddEdit;
using HRMS.Application.FeatureValidators.RecruitmentProcess;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.FeatureValidators.Organisation
{
    public class AddEditOrganisationLocationCommandValidator : AbstractValidator<AddEditOrganisationLocationCommand>
    {
        public AddEditOrganisationLocationCommandValidator(IStringLocalizer<AddEditOrganisationLocationCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
        }
    }
}

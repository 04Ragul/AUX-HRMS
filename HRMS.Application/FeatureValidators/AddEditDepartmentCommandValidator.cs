using FluentValidation;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.FeatureValidators
{
    public class AddEditDepartmentCommandValidator : AbstractValidator<AddEditDeparmentCommand>
    {
        public AddEditDepartmentCommandValidator(IStringLocalizer<AddEditDepartmentCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Code)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Code is required!"]);
        }
    }
}

using FluentValidation;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.AddEdit;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.FeatureValidators.RecruitmentProcess
{
    public class AddEditJobLocationCommandValidator : AbstractValidator<AddEditJobLocationCommand>
    {
        public AddEditJobLocationCommandValidator(IStringLocalizer<AddEditJobLocationCommandValidator> localizer)
        {
            RuleFor(request => request.Country)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Country is required!"]);
        }
    }
}

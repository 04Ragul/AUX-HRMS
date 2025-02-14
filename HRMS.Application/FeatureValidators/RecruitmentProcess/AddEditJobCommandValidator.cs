using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.Jobs.Commands.AddEdit;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.FeatureValidators.RecruitmentProcess
{
    public class AddEditJobCommandValidator : AbstractValidator<AddEditJobCommand>
    {
        public AddEditJobCommandValidator(IStringLocalizer<AddEditJobCommandValidator> localizer)
        {
            RuleFor(request => request.Title)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Title is required!"]);
        }
    }
}

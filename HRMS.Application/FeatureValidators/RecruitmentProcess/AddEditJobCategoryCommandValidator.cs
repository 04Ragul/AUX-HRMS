using FluentValidation;
using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.FeatureValidators.RecruitmentProcess
{
    public class AddEditJobCategoryCommandValidator : AbstractValidator<AddEditJobCategoryCommand>
    {
        public AddEditJobCategoryCommandValidator(IStringLocalizer<AddEditJobCategoryCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
        }
    }
}

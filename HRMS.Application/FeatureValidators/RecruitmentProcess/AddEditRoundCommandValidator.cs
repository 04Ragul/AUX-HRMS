using FluentValidation;
using HRMS.Application.Features.RecruitmentProcess.Rounds.Commands.AddEdit;
using Microsoft.Extensions.Localization;

namespace HRMS.Application.FeatureValidators.RecruitmentProcess
{
    public class AddEditRoundCommandValidator : AbstractValidator<AddEditRoundCommand>
    {
        public AddEditRoundCommandValidator(IStringLocalizer<AddEditRoundCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
        }
    }
}

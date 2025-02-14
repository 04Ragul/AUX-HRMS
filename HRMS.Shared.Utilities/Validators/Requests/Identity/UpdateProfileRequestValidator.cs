using FluentValidation;
using Microsoft.Extensions.Localization;
using HRMS.Shared.Utilities.Requests.Identity;

namespace HRMS.Shared.Utilities.Validators.Requests.Identity
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator(IStringLocalizer<UpdateProfileRequestValidator> localizer)
        {
            _ = RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required"]);
            _ = RuleFor(request => request.UserName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["User Name is required"]);
        }
    }
}

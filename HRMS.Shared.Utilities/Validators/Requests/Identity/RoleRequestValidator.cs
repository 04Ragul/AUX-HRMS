using FluentValidation;
using Microsoft.Extensions.Localization;
using HRMS.Shared.Utilities.Requests.Identity;

namespace HRMS.Shared.Utilities.Validators.Requests.Identity
{
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator(IStringLocalizer<RoleRequestValidator> localizer)
        {
            _ = RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required"]);
        }
    }
}

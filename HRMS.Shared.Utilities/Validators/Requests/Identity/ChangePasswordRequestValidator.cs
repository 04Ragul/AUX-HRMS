﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using HRMS.Shared.Utilities.Requests.Identity;

namespace HRMS.Shared.Utilities.Validators.Requests.Identity
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator(IStringLocalizer<ChangePasswordRequestValidator> localizer)
        {
            _ = RuleFor(request => request.OldPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Current Password is required!"]);
            _ = RuleFor(request => request.NewPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"])
                .MinimumLength(8).WithMessage(localizer["Password must be at least of length 8"])
                .Matches(@"[A-Z]").WithMessage(localizer["Password must contain at least one capital letter"])
                .Matches(@"[a-z]").WithMessage(localizer["Password must contain at least one lowercase letter"])
                .Matches(@"[0-9]").WithMessage(localizer["Password must contain at least one digit"]);
            _ = RuleFor(request => request.ConfirmNewPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password Confirmation is required!"])
                .Equal(request => request.NewPassword).WithMessage(x => localizer["Passwords don't match"]);
        }
    }
}

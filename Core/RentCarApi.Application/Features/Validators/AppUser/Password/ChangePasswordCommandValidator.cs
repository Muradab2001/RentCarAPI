using FluentValidation;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands;

namespace RentCarApi.Application.Features.Validators.AppUser.Password
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommandRequest>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("AppUserId must be a positive integer.");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old Password is required.")
                .MinimumLength(6).WithMessage("Old Password must be at least 6 characters long.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.")
                .MinimumLength(6).WithMessage("New Password must be at least 6 characters long.");

            RuleFor(x => x.NewPasswordConfirm)
                .NotEmpty().WithMessage("New Password Confirmation is required.")
                .Equal(x => x.NewPassword).WithMessage("New Password and Confirmation Password must match.");
        }
    }
}
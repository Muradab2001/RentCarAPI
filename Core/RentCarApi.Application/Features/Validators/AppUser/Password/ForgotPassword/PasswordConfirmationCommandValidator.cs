using FluentValidation;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;

namespace RentCarApi.Application.Features.Validators.AppUser.Password.ForgotPassword
{
    public class PasswordConfirmationCommandValidator : AbstractValidator<PasswordConfirmationCommandRequest>
    {
        public PasswordConfirmationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.")
                .MinimumLength(6).WithMessage("New Password must be at least 6 characters long.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm New Password is required.")
                .Equal(x => x.NewPassword).WithMessage("New Password and Confirm New Password must match.");
        }
    }
}
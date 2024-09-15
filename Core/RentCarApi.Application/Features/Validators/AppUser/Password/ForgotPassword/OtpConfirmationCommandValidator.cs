using FluentValidation;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;

namespace RentCarApi.Application.Features.Validators.AppUser.Password.ForgotPassword
{
    public class OtpConfirmationCommandValidator : AbstractValidator<OtpConfirmationCommandRequest>
    {
        public OtpConfirmationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required.")
                .Length(6).WithMessage("OTP must be 6 characters long.");
        }
    }
}
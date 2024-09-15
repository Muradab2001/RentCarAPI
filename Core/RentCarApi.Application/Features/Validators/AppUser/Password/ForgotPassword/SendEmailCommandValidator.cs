using FluentValidation;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;

namespace RentCarApi.Application.Features.Validators.AppUser.Password.ForgotPassword
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommandRequest>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
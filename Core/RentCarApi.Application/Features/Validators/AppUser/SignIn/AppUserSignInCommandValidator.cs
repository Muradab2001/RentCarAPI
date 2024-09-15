using FluentValidation;
using RentCarApi.Application.Features.Commands.AppUser.SignIn;

namespace RentCarApi.Application.Features.Validators.AppUser.SignIn
{
    public class AppUserSignInCommandValidator : AbstractValidator<AppUserSignInCommandRequest>
    {
        public AppUserSignInCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
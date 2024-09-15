using FluentValidation;
using RentCarApi.Application.DTOs;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.SignUp;
using System.Text.RegularExpressions;

namespace RentCarApi.Application.Features.Validators.AppUser.Personal
{
    public partial class PersonalCreateCommandValidator : AbstractValidator<AppUserPersonalSignUpCommandRequest>
    {
        public PersonalCreateCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(MyRegex()).WithMessage("Invalid phone number format. It should be 10-15 digits.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.RepeatPassword)
                .NotEmpty().WithMessage("Repeat Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Repeat Password must match.");

            RuleFor(x => x.PersonalData)
                .SetValidator(new PersonalDataDTOValidator());

            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("LocationId must be a positive integer.");
        }

        [GeneratedRegex(@"^(?:0|994)(?:12|51|50|55|70|77)[^\w]{0,2}[2-9][0-9]{2}[^\w]{0,2}[0-9]{2}[^\w]{0,2}[0-9]{2}$")]
        private static partial Regex MyRegex();
    }
}
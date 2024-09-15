using FluentValidation;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.Update;

namespace RentCarApi.Application.Features.Validators.AppUser.Personal
{
    public class PersonalUpdateCommandValidator : AbstractValidator<AppUserPersonalUpdateCommandRequest>
    {
        public PersonalUpdateCommandValidator()
        {
            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("AppUserId must be a positive integer.");

            RuleFor(x => x.PersonalData)
                .NotNull().WithMessage("PersonalData is required.")
                .SetValidator(new PersonalDataDTOValidator());

            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("LocationId must be a positive integer.");
        }
    }
}
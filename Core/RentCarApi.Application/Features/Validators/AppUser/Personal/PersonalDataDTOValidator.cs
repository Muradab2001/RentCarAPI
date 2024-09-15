using FluentValidation;
using RentCarApi.Application.DTOs;

namespace RentCarApi.Application.Features.Validators.AppUser.Personal
{
    public class PersonalDataDTOValidator : AbstractValidator<PersonalDataDTO>
    {
        public PersonalDataDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot be longer than 50 characters.");

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100.");

            RuleFor(x => x.IdCardImage)
                .NotNull().WithMessage("ID Card Image is required.");

            RuleFor(x => x.DrivingLicenseImage).
                NotNull().WithMessage("Driving License Image must be a valid image file.");
        }
    }
}
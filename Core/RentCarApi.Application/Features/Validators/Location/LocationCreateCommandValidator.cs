using FluentValidation;
using RentCarApi.Application.Features.Commands.Location.Create;

namespace RentCarApi.Application.Features.Validators.Location;
public class LocationCreateCommandValidator : AbstractValidator<LocationCreateCommandRequest>
{
    public LocationCreateCommandValidator()
    {
        RuleFor(l => l.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name maximum lenght is 50 characters.");
    }
}

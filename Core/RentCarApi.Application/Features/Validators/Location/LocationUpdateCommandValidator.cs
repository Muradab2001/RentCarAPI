using FluentValidation;
using RentCarApi.Application.Features.Commands.Location.Update;

namespace RentCarApi.Application.Features.Validators.Location;
public class LocationUpdateCommandValidator : AbstractValidator<LocationUpdateCommandRequest>
{
    public LocationUpdateCommandValidator()
    {
        RuleFor(l => l.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(l => l.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name maximum lenght is 50 characters.");
    }
}

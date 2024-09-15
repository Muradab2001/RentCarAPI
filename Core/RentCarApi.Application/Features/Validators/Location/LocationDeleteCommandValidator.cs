using FluentValidation;
using RentCarApi.Application.Features.Commands.Location.Delete;

namespace RentCarApi.Application.Features.Validators.Location;
public class LocationDeleteCommandValidator : AbstractValidator<LocationDeleteCommandRequest>
{
    public LocationDeleteCommandValidator()
    {
        RuleFor(l => l.Id).NotEmpty();
    }
}

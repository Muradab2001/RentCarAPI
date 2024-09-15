using FluentValidation;
using RentCarApi.Application.Features.Commands.VehicleType.Create;

namespace RentCarApi.Application.Features.Validators.VehicleType;
public class VehicleTypeCreateCommandValidator : AbstractValidator<VehicleTypeCreateCommandRequest>
{
    public VehicleTypeCreateCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name maximum lenght is 50 characters.");
    }
}

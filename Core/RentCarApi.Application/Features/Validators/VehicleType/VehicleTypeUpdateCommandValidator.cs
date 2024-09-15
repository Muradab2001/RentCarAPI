using FluentValidation;
using RentCarApi.Application.Features.Commands.VehicleType.Update;

namespace RentCarApi.Application.Features.Validators.VehicleType;
public class VehicleTypeUpdateCommandValidator : AbstractValidator<VehicleTypeUpdateCommandRequest>
{
    public VehicleTypeUpdateCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name maximum lenght is 50 characters.");
    }
}

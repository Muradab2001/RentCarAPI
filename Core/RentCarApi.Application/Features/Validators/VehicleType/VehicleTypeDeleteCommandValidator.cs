using FluentValidation;
using RentCarApi.Application.Features.Commands.VehicleType.Delete;

namespace RentCarApi.Application.Features.Validators.VehicleType;
public class VehicleTypeDeleteCommandValidator : AbstractValidator<VehicleTypeDeleteCommandRequest>
{
    public VehicleTypeDeleteCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

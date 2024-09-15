using FluentValidation;
using RentCarApi.Application.Features.Commands.Car.Delete;

namespace RentCarApi.Application.Features.Validators.Car;
public class CarDeleteCommandValidator : AbstractValidator<CarDeleteCommandRequest>
{
    public CarDeleteCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}

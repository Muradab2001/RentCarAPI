using FluentValidation;
using RentCarApi.Application.Features.Commands.BabySeat.Delete;

namespace RentCarApi.Application.Features.Validators.BabySeat;
public class BabySeatDeleteCommandValidator : AbstractValidator<BabySeatDeleteCommandRequest>
{
    public BabySeatDeleteCommandValidator()
    {
        RuleFor(b => b.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}

using FluentValidation;
using RentCarApi.Application.Features.Commands.PromoCode.Create;

namespace RentCarApi.Application.Features.Validators.PromoCode;
public class PromoCodeCreateCommandValidator : AbstractValidator<PromoCodeCreateCommandRequest>
{
    public PromoCodeCreateCommandValidator()
    {
        RuleFor(l => l.Code).NotEmpty().WithMessage("Code is required.")
        .MaximumLength(50).WithMessage("Code maximum lenght is 50 characters.");
    }
}

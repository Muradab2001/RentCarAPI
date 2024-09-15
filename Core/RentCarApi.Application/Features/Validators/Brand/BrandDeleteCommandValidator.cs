using FluentValidation;
using RentCarApi.Application.Features.Commands.Brand.Delete;

namespace RentCarApi.Application.Features.Validators.Brand;
public class BrandDeleteCommandValidator : AbstractValidator<BrandDeleteCommandRequest>
{
    public BrandDeleteCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}

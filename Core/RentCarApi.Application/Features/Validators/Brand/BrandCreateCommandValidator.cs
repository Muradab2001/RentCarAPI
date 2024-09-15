using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentCarApi.Application.Features.Commands.Brand.Create;
using RentCarApi.Application.Helpers;

namespace RentCarApi.Application.Features.Validators.Brand;
public class BrandCreateCommandValidator : AbstractValidator<BrandCreateCommandRequest>
{
    public BrandCreateCommandValidator()
    {
        RuleFor(b => b.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name maximum lenght is 50 characters.");
        RuleFor(x => x.Image)
           .NotNull().WithMessage("Image is required.")
           .Must(BeAValidImage).WithMessage("Invalid image format or size.");
    }
    private bool BeAValidImage(IFormFile image)
    {

        if (!image.ImageIsOkay(4))
        {
            return false;
        }

        return true;
    }

}

using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentCarApi.Application.Features.Commands.Brand.Update;
using RentCarApi.Application.Helpers;

namespace RentCarApi.Application.Features.Validators.Brand;
public class BrandUpdateCommandValidator : AbstractValidator<BrandUpdateCommandRequest>
{
    public BrandUpdateCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(b => b.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name maximum lenght is 50 characters.");
        RuleFor(x => x.Image)
           .Must(BeAValidImage).WithMessage("Invalid image format or size.");
    }
    private bool BeAValidImage(IFormFile image)
    {
        if(image != null) 
        {
            if (!image.ImageIsOkay(4))
            {
                return false;
            }
        }
        return true;
    }
}

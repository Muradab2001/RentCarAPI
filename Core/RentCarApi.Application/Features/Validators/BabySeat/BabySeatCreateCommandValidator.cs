using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentCarApi.Application.Features.Commands.BabySeat.Create;
using RentCarApi.Application.Helpers;

namespace RentCarApi.Application.Features.Validators.BabySeat;
public class BabySeatCreateCommandValidator : AbstractValidator<BabySeatCreateCommandRequest>
{
    public BabySeatCreateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.AppUserId).NotEmpty().WithMessage("AppUserId is required.");
        RuleForEach(x => x.Images).Must(BeAValidImage).WithMessage("Invalid image format or size.");
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

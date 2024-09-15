using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentCarApi.Application.Features.Commands.BabySeat.Update;
using RentCarApi.Application.Helpers;

namespace RentCarApi.Application.Features.Validators.BabySeat;
public class BabySeatUpdateCommandValidator : AbstractValidator<BabySeatUpdateCommandRequest>
{
    public BabySeatUpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleForEach(x => x.ImagesToAdd)
            .Must(BeAValidImage);

    }
    private bool BeAValidImage(IFormFile image)
    {
        if (image != null)
        {
            if (!image.ImageIsOkay(4))
            {
                return false;
            }
        }
        return true;
    }
}

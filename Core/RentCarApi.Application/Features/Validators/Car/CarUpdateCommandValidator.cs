using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentCarApi.Application.Features.Commands.Car.Update;
using RentCarApi.Application.Helpers;

namespace RentCarApi.Application.Features.Validators.Car;
public class CarUpdateCommandValidator : AbstractValidator<CarUpdateCommandRequest>
{
    public CarUpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        RuleFor(x => x.SeatCount).GreaterThan(0);
        RuleFor(x => x.Year).InclusiveBetween(1900, 2100);
        RuleFor(x => x.Power).GreaterThan(0);
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.TransmissionType).IsInEnum();
        RuleFor(x => x.FuelType).IsInEnum();
        RuleFor(x => x.ColorId).NotEmpty();
        RuleFor(x => x.ModelId).NotEmpty();
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.BrandId).NotEmpty();
        RuleFor(x => x.VehicleTypeId).NotEmpty();
        RuleForEach(x => x.ImagesToAdd).Must((request, images) => BeAValidImages(request.ImagesToAdd)).WithMessage("Invalid image format or size.");
    }
    private bool BeAValidImages(List<IFormFile> images)
    {
        foreach (var image in images)
        {
            if (image != null)
            {
                if (!image.ImageIsOkay(4))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
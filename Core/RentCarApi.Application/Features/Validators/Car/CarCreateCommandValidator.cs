using FluentValidation;
using RentCarApi.Application.Features.Commands.Car.Create;

namespace RentCarApi.Application.Features.Validators.Car;
public class CarCreateCommandValidator : AbstractValidator<CarCreateCommandRequest>
{
    public CarCreateCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");

        RuleFor(x => x.SeatCount)
            .GreaterThan(0).WithMessage("Seat count must be greater than 0.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Year must be between 1900 and {DateTime.Now.Year}.");

        RuleFor(x => x.Power)
            .GreaterThan(0).WithMessage("Power must be greater than 0.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.TransmissionType)
            .IsInEnum().WithMessage("Invalid transmission type.");

        RuleFor(x => x.FuelType)
            .IsInEnum().WithMessage("Invalid fuel type.");

        RuleFor(x => x.AppUserId)
            .GreaterThan(0).WithMessage("Invalid user ID.");

        RuleFor(x => x.ColorId)
            .GreaterThan(0).WithMessage("Invalid color ID.");

        RuleFor(x => x.ModelId)
            .GreaterThan(0).WithMessage("Invalid model ID.");

        RuleFor(x => x.LocationId)
            .GreaterThan(0).WithMessage("Invalid location ID.");

        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("Invalid brand ID.");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0).WithMessage("Invalid vehicle type ID.");

        RuleFor(x => x.SuppliesId)
            .Must(ids => ids != null && ids.Count > 0).WithMessage("At least one supply must be selected.");

        RuleFor(x => x.Images)
            .Must(images => images != null && images.Count > 0).WithMessage("At least one image is required.");
    }
}
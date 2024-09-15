using FluentValidation;
using RentCarApi.Application.Features.Commands.Favorite.Create;

namespace RentCarApi.Application.Features.Validators.Favorite
{
    public class AddToFavoriteCommandValidator : AbstractValidator<AddToFavoriteCommandRequest>
    {
        public AddToFavoriteCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be a positive integer.");

            RuleFor(x => x.CarId)
                .GreaterThan(0).WithMessage("CarId must be a positive integer.");
        }
    }
}
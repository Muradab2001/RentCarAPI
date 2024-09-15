using FluentValidation;
using RentCarApi.Application.Features.Commands.Favorite.Delete;

namespace RentCarApi.Application.Features.Validators.Favorite
{
    public class RemoveFromFavoriteCommandValidator : AbstractValidator<RemoveFromFavoriteCommandRequest>
    {
        public RemoveFromFavoriteCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be a positive integer.");

            RuleFor(x => x.CarId)
                .GreaterThan(0).WithMessage("CarId must be a positive integer.");
        }
    }
}
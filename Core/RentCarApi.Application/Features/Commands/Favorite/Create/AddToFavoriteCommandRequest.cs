using MediatR;

namespace RentCarApi.Application.Features.Commands.Favorite.Create
{
    public class AddToFavoriteCommandRequest : IRequest<AddToFavoriteCommandResponse>
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
    }
}
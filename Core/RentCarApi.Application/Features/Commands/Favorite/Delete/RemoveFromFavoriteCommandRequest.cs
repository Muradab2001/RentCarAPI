using MediatR;

namespace RentCarApi.Application.Features.Commands.Favorite.Delete
{
    public class RemoveFromFavoriteCommandRequest : IRequest<RemoveFromFavoriteCommandResponse>
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
    }
}
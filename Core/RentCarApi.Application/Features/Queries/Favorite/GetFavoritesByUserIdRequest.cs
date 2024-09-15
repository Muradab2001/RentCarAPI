using MediatR;

namespace RentCarApi.Application.Features.Queries.Favorite
{
    public class GetFavoritesByUserIdRequest : IRequest<List<GetFavoritesByUserIdResponse>>
    {
        public int UserId {  get; set; }
    }
}
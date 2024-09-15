namespace RentCarApi.Application.Features.Queries.Favorite
{
    public class GetFavoritesByUserIdResponse
    {
        public List<FavoriteResponse> Favorites { get; set; }
    }
}
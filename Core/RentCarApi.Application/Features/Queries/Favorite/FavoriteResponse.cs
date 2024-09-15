using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Queries.Favorite
{
    public class FavoriteResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<ImageResponse> Images { get; set; }
    }
}
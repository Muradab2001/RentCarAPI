using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Favorite : BaseEntity
    {
        public Car Car { get; set; }
        public int CarId { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }
    }
}
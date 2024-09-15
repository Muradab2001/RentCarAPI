using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class CarDiscount : IBaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
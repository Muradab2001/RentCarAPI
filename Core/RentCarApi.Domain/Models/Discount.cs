using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Discount : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CarDiscount> CarDiscounts { get; set; }
    }
}
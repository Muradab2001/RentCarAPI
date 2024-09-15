using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Order : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalAmount { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public bool isConfirm { get; set; }
    }
}
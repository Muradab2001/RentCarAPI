using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models;
public class Review : BaseEntity
{
    public byte Rate { get; set; }
    public int CarId { get; set; }
    public Car Car {  get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}

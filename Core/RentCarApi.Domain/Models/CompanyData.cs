using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models;

public class CompanyData : BaseEntity
{
    public AppUser AppUser { get; set; }
    public string Name { get; set; }
    public int AppUserId { get; set; }
    public string Image { get; set; }
    public List<Car> Cars { get; set; }
    public List<PromoCode> PromoCodes { get; set; }
    public BabySeat BabySeat { get; set; }
}
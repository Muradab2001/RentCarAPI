using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
        public List<AppUser> Users { get; set; }
    }
}
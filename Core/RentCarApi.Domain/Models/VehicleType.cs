using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class VehicleType : BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}
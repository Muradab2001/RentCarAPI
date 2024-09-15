using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
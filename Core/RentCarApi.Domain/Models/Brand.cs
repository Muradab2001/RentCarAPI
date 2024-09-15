using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Car> Cars { get; set; }
        public List<Model> Models { get; set; }
    }
}
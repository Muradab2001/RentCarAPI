using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Supply : BaseEntity
    {
        public string Name { get; set; }
        public List<CarSupply> Cars { get; set; } = [];
    }
}
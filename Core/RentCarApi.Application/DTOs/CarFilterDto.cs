using RentCarApi.Domain.Enum;

namespace RentCarApi.Application.DTOs
{
    public class CarFilterDto
    {
        public string? Brand { get; set; }
        public List<int>? ModelIds { get; set; }
        public int? VehicleTypeId { get; set; }
        public double? MinPrice {  get; set; }
        public double?  MaxPrice { get; set; }
        public List<FuelType>? FuelTypes { get; set; }
        public List<TransmissionType>? TransmissionTypes { get; set; }
        public int? MinSeatCount { get; set; }
        public int? MaxSeatCount { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public List<int>? ColorIds { get; set; }
    }
}
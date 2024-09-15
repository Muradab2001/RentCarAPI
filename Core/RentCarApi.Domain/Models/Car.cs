using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models;
public class Car : BaseEntity
{
    public string Description { get; set; }
    public int SeatCount { get; set; }
    public int ReservedCount { get; set; }
    public bool isReserved { get; set; }
    public int Year { get; set; }
    public int Power { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }
    public double DiscountPrice {  get; set; }
    public TransmissionType TransmissionType { get; set; }
    public FuelType FuelType { get; set; }
    public AppUser AppUser { get; set; }
    public int? AppUserId { get; set; }
    public int? ColorId { get; set; }
    public Color Color { get; set; }
    public int? ModelId { get; set; }
    public int? LocationId { get; set; }
    public Location Location { get; set; }
    public Model Model { get; set; }
    public int? BrandId { get; set; }
    public Brand Brand { get; set; }
    public int? VehicleTypeId { get; set; }
    public VehicleType VehicleType { get; set; }
    public List<CarSupply> Supplies { get; set; } = [];
    public List<Order> Orders { get; set; } = [];

    private readonly List<Image<Car>> _images;
    public IReadOnlyCollection<Image<Car>> Images => _images;
    public List<CarDiscount> CarDiscounts { get; set; }
    public List<Review> Reviews { get; set; }
    public Car()
    {
        _images = [];
    }

    public void AddSupply(List<Supply> supplies)
    {
        Supplies ??= [];

        var carSuppliesToAdd = supplies.Select(supply =>
        {
            var carSupply = new CarSupply();
            carSupply.SetSupply(supply.Id);
            return carSupply;
        });
        Supplies.AddRange(carSuppliesToAdd);
    }
}
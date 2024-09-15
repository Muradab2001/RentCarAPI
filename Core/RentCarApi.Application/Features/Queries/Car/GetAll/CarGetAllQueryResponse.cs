using RentCarApi.Application.Features.Queries.Brend;
using RentCarApi.Application.Features.Queries.Favorite;
using RentCarApi.Application.Features.Queries.Location.GetAll;
using RentCarApi.Application.Features.Queries.Model;
using RentCarApi.Application.Features.Queries.VehicleType;
using RentCarApi.Domain.Enum;

namespace RentCarApi.Application.Features.Queries.Car.GetAll;
public class CarGetAllQueryResponse
{
    public int Id { get; set; }
    public int SeatCount { get; set; }
    public bool isReserved { get; set; }
    public int Year { get; set; }
    public int Power { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public FuelType FuelType { get; set; }
    public LocationGetAllQueryResponse Location { get; set; }
    public ModelGetAllQueryResponse Model { get; set; }
    public BrandGetAllQueryResponse Brand { get; set; }
    public VehicleTypeGetAllQueryResponse VehicleType { get; set; }
    public List<ImageResponse> Images { get; set; }
}
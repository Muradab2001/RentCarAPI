using MediatR;
using Microsoft.AspNetCore.Http;
using RentCarApi.Domain.Enum;

namespace RentCarApi.Application.Features.Commands.Car.Update;
public class CarUpdateCommandRequest : IRequest<CarUpdateCommandResponse>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int SeatCount { get; set; }
    public int Year { get; set; }
    public int Power { get; set; }
    public double Weight { get; set; }
    public double Price { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public FuelType FuelType { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int LocationId { get; set; }
    public int BrandId { get; set; }
    public int VehicleTypeId { get; set; }
    public List<int>? SuppliesIdsToAdd { get; set; }
    public List<int>? SuppliesIdsToDelete { get; set; }
    public List<int>? ImagesToDelete { get; set; }
    public List<IFormFile>? ImagesToAdd { get; set; }
}

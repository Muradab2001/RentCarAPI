using MediatR;
using RentCarApi.Application.Features.Response;
using RentCarApi.Domain.Enum;

namespace RentCarApi.Application.Features.Queries.Car.GetAll;
public class CarGetAllQueryRequest : IRequest<Pagination<CarGetAllQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Brand { get; set; }
    public List<int>? ModelIds { get; set; }
    public int? VehicleTypeId { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public List<FuelType>? FuelTypes { get; set; }
    public List<TransmissionType>? TransmissionTypes { get; set; }
    public List<int>? SeatCounts { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public List<int>? ColorIds { get; set; }
    public int? LocationId { get; set; }
    public DateTime? PickupDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
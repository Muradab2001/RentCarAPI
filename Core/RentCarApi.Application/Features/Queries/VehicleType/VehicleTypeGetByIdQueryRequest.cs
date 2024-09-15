using MediatR;

namespace RentCarApi.Application.Features.Queries.VehicleType;
public class VehicleTypeGetByIdQueryRequest : IRequest<VehicleTypeGetByIdQueryResponse>
{
    public int Id {  get; set; }
}

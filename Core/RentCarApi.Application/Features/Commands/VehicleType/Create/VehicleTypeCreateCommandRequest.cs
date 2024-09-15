using MediatR;

namespace RentCarApi.Application.Features.Commands.VehicleType.Create;
public class VehicleTypeCreateCommandRequest : IRequest<VehicleTypeCreateCommandResponse>
{
    public string Name { get; set; }
}

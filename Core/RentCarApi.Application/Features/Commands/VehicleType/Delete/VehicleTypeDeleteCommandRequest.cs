using MediatR;

namespace RentCarApi.Application.Features.Commands.VehicleType.Delete;
public class VehicleTypeDeleteCommandRequest : IRequest<VehicleTypeDeleteCommandResponse>
{
   public int Id { get; set; }
}

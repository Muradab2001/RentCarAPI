using MediatR;

namespace RentCarApi.Application.Features.Commands.Car.Delete;
public class CarDeleteCommandRequest : IRequest<CarDeleteCommandResponse>
{
    public int Id { get; set; }
}

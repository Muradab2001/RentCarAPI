using MediatR;

namespace RentCarApi.Application.Features.Commands.Order.Create;
public class OrderCreateCommandRequest : IRequest<OrderCreateCommandResponse>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CarId { get; set; }
    public int AppUserId { get; set; }
}

using MediatR;

namespace RentCarApi.Application.Features.Commands.Review.Create;
public class ReviewCreateCommandRequest : IRequest<ReviewCreateCommandResponse>
{
    public byte Rate { get; set; }
    public int CarId { get; set; }
    public int AppUserId { get; set; }
}

using MediatR;

namespace RentCarApi.Application.Features.Commands.Review.Delete;
public class ReviewDeleteCommandRequest : IRequest<ReviewDeleteCommandResponse>
{
    public int Id { get; set; }
}
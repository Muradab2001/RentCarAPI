using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.Review.Delete;
public class ReviewDeleteCommandResponse
{
    public int Id { get; set; }
    public string Message { get; set; } = ResponseMessages.Success;
}

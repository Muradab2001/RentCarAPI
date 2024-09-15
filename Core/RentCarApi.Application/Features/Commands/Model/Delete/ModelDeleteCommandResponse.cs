using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.Model.Delete;
public class ModelDeleteCommandResponse
{
    public int Id { get; set; }
    public string Message { get; set; } = ResponseMessages.Success;
}

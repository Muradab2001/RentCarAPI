using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.Model.Update;
public class ModelUpdateCommandResponse
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}

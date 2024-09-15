using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.BabySeat.Create;
public class BabySeatCreateCommandResponse
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}

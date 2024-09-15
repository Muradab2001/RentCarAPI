using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.Car.Update;
public class CarUpdateCommandResponse
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}

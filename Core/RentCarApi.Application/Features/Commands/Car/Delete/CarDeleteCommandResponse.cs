using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.Car.Delete;
public class CarDeleteCommandResponse 
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}

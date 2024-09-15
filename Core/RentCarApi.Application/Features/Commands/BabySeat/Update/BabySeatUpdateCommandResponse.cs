using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.BabySeat.Update
{
    public class BabySeatUpdateCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = ResponseMessages.Success;
    }
}
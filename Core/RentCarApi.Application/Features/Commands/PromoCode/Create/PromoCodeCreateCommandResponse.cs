using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.PromoCode.Create;
public class PromoCodeCreateCommandResponse
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}

using MediatR;

namespace RentCarApi.Application.Features.Commands.PromoCode.Create;
public class PromoCodeCreateCommandRequest : IRequest<PromoCodeCreateCommandResponse>
{
    public string Code { get; set; }
    public int AppUserId { get; set; }
}

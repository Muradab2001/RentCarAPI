using MediatR;
using Microsoft.AspNetCore.Http;

namespace RentCarApi.Application.Features.Commands.BabySeat.Create;
public class BabySeatCreateCommandRequest : IRequest<BabySeatCreateCommandResponse>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int AppUserId { get; set; }
    public List<IFormFile>? Images { get; set; }
}
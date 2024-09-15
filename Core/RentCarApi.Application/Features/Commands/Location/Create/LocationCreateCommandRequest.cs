using MediatR;

namespace RentCarApi.Application.Features.Commands.Location.Create
{
    public class LocationCreateCommandRequest : IRequest<LocationCreateCommandResponse>
    {
        public string Name {  get; set; }
    }
}
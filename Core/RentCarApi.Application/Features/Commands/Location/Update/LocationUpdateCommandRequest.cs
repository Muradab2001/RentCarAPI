using MediatR;

namespace RentCarApi.Application.Features.Commands.Location.Update
{
    public class LocationUpdateCommandRequest : IRequest<LocationUpdateCommandResponse>
    {
        public int Id { get; set; }
        public string Name {  get; set; }
    }
}
using MediatR;

namespace RentCarApi.Application.Features.Commands.Location.Delete
{
    public class LocationDeleteCommandRequest : IRequest<LocationDeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}
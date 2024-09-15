using MediatR;

namespace RentCarApi.Application.Features.Commands.Color.Update
{
    public class ColorUpdateCommandRequest : IRequest<ColorUpdateCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
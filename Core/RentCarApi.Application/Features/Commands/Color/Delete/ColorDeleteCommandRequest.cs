using MediatR;

namespace RentCarApi.Application.Features.Commands.Color.Delete
{
    public class ColorDeleteCommandRequest : IRequest<ColorDeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}
using MediatR;

namespace RentCarApi.Application.Features.Commands.Brand.Delete
{
    public class BrandDeleteCommandRequest : IRequest<BrandDeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}

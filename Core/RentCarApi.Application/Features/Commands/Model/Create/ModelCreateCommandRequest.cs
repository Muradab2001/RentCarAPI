using MediatR;

namespace RentCarApi.Application.Features.Commands.Model.Create;
public class ModelCreateCommandRequest : IRequest<ModelCreateCommandResponse>
{
    public string Name { get; set; }
    public int BrandId { get; set; }
}

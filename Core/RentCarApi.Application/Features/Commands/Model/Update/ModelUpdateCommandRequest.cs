using MediatR;

namespace RentCarApi.Application.Features.Commands.Model.Update;
public class ModelUpdateCommandRequest : IRequest<ModelUpdateCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
}

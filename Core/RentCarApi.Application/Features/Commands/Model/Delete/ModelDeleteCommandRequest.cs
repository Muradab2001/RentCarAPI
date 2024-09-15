using MediatR;

namespace RentCarApi.Application.Features.Commands.Model.Delete;
public class ModelDeleteCommandRequest : IRequest<ModelDeleteCommandResponse>
{
    public int Id { get; set; }
}

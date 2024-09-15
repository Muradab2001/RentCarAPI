using MediatR;

namespace RentCarApi.Application.Features.Queries.Supply.GetById;
public class SupplyGetByIdQueryRequest : IRequest<SupplyGetByIdQueryResponse>
{
    public int Id { get; set; }
}

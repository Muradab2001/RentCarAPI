using MediatR;

namespace RentCarApi.Application.Features.Queries.Supply.GetAll;
public class SupplyGetAllQueryRequest : IRequest<IList<SupplyGetAllQueryResponse>>
{
}

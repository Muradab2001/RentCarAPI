using MediatR;

namespace RentCarApi.Application.Features.Queries.Location.GetAll;
public class LocationGetAllQueryRequest : IRequest<IList<LocationGetAllQueryResponse>>
{
}

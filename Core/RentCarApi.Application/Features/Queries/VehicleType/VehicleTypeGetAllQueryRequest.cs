using MediatR;

namespace RentCarApi.Application.Features.Queries.VehicleType;
public class VehicleTypeGetAllQueryRequest : IRequest<IList<VehicleTypeGetAllQueryResponse>>
{
}

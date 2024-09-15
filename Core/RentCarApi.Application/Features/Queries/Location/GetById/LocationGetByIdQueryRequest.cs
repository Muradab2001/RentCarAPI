using MediatR;
using RentCarApi.Application.Features.Queries.Location.GetById;

namespace RentCarApi.Application.Features.Queries.Location.GetAll;
public class LocationGetByIdQueryRequest : IRequest<LocationGetByIdQueryResponse>
{
    public int Id { get; set; }
}

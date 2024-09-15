using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Location.GetAll;
using RentCarApi.Application.Features.Queries.Location.GetById;
using RentCarApi.Application.Features.Queries.Supply.GetById;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Location;
public class LocationGetByIdQueryHandler : IRequestHandler<LocationGetByIdQueryRequest, LocationGetByIdQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public LocationGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<LocationGetByIdQueryResponse> Handle(LocationGetByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new LocationGetByIdQueryResponse();
        var location = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetAsync(l => l.Id == request.Id);
        if (location is not null)
        {
            response = _mapper.Map<LocationGetByIdQueryResponse>(location);
        }
        return response;
    }
}

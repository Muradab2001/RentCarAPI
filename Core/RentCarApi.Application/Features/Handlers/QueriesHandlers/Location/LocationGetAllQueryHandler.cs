using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Location.GetAll;
using RentCarApi.Application.Features.Queries.Supply.GetAll;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Location;
public class LocationGetAllQueryHandler : IRequestHandler<LocationGetAllQueryRequest,IList<LocationGetAllQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public LocationGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<LocationGetAllQueryResponse>> Handle(LocationGetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetAll(tracking: false);
        var response = _mapper.Map<IList<LocationGetAllQueryResponse>>(locations);
        return response;
    }
}

using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.VehicleType;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.VehicleType;
public class VehicleTypeGetByIdQueryHandler : IRequestHandler<VehicleTypeGetByIdQueryRequest, VehicleTypeGetByIdQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleTypeGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VehicleTypeGetByIdQueryResponse> Handle(VehicleTypeGetByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new VehicleTypeGetByIdQueryResponse();
        var vehicleType = await _unitOfWork.GetReadRepository<Domain.Models.VehicleType>().GetAsync(vt => vt.Id == request.Id);
        if (vehicleType is not null)
        {
            response = _mapper.Map<VehicleTypeGetByIdQueryResponse>(vehicleType);
        }
        return response;
    }
}

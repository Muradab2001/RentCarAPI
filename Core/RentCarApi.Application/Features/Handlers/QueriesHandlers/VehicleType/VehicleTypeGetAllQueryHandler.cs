using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.VehicleType;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.VehicleType;
public class VehicleTypeGetAllQueryHandler : IRequestHandler<VehicleTypeGetAllQueryRequest, IList<VehicleTypeGetAllQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public VehicleTypeGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<VehicleTypeGetAllQueryResponse>> Handle(VehicleTypeGetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var vehicleTypes = await _unitOfWork.GetReadRepository<Domain.Models.VehicleType>().GetAll();
        var response = _mapper.Map<IList<VehicleTypeGetAllQueryResponse>>(vehicleTypes);
        return response;
    }
}

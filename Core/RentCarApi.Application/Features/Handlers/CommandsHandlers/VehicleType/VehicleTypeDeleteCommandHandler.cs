using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.VehicleType.Delete;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.VehicleType;
public class VehicleTypeDeleteCommandHandler : IRequestHandler<VehicleTypeDeleteCommandRequest, VehicleTypeDeleteCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public VehicleTypeDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<VehicleTypeDeleteCommandResponse> Handle(VehicleTypeDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new VehicleTypeDeleteCommandResponse();
        var vehicleType = await _unitOfWork.GetReadRepository<Domain.Models.VehicleType>().GetAsync(v => v.Id == request.Id);
        if (vehicleType == null)  response.Message = "Not found";
        else
        {
            await _unitOfWork.GetWriteRepository<Domain.Models.VehicleType>().RemoveAsync(vehicleType);
            if (await _unitOfWork.SaveChangesAsync() < 1) response.Message = "Something went Wrong";
            response.Id = request.Id;
        }
        return response;
    }
}

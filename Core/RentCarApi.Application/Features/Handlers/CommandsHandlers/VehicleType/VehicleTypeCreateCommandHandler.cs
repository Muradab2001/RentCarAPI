using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.VehicleType.Create;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.VehicleType;
public class VehicleTypeCreateCommandHandler : IRequestHandler<VehicleTypeCreateCommandRequest, VehicleTypeCreateCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public VehicleTypeCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<VehicleTypeCreateCommandResponse> Handle(VehicleTypeCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new VehicleTypeCreateCommandResponse();
        var vehicleType = _mapper.Map<Domain.Models.VehicleType>(request);
        await _unitOfWork.GetWriteRepository<Domain.Models.VehicleType>().AddAsync(vehicleType);

        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            response.Succeeded = false;
            response.Message = "Something went wrong";
        }
        return response;
    }
}

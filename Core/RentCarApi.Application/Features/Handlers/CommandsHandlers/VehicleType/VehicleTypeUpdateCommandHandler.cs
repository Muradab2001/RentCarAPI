using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.VehicleType.Delete;
using RentCarApi.Application.Features.Commands.VehicleType.Update;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.VehicleType
{
    public class VehicleTypeUpdateCommandHandler : IRequestHandler<VehicleTypeUpdateCommandRequest, VehicleTypeUpdateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public VehicleTypeUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<VehicleTypeUpdateCommandResponse> Handle(VehicleTypeUpdateCommandRequest request, CancellationToken cancellationToken)
        {
           var response =new VehicleTypeUpdateCommandResponse();
           var vehicleType = _mapper.Map<Domain.Models.VehicleType>(request);
           var isSuccess =_unitOfWork.GetWriteRepository<Domain.Models.VehicleType>().Update(vehicleType);
           if(await _unitOfWork.SaveChangesAsync()<1 || !isSuccess) {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
           return response;
        }
    }
}

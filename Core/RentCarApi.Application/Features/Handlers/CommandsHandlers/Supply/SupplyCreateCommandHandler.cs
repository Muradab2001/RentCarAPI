using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Color.Create;
using RentCarApi.Application.Features.Commands.Model.Create;
using RentCarApi.Application.Features.Commands.Supply.Create;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Supply
{
    public class SupplyCreateCommandHandler : IRequestHandler<SupplyCreateCommandRequest, SupplyCreateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SupplyCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<SupplyCreateCommandResponse> Handle(SupplyCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SupplyCreateCommandResponse();
            var supply = _mapper.Map<Domain.Models.Supply>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Supply>().AddAsync(supply);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
            response.Succeeded = false;
            response.Message = "Something went wrong";
            }
            
            return response;
        }
    }
}

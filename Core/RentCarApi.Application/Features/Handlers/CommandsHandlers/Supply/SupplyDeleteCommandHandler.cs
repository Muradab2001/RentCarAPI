using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Model.Delete;
using RentCarApi.Application.Features.Commands.Supply.Delete;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Supply
{
    public class SupplyDeleteCommandHandler : IRequestHandler<SupplyDeleteCommandRequest, SupplyDeleteCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SupplyDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplyDeleteCommandResponse> Handle(SupplyDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SupplyDeleteCommandResponse();
            var supply = _mapper.Map<Domain.Models.Supply>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Supply>().RemoveAsync(supply);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Message = ResponseMessages.Failed;
            }
            return response;
        }
    }
}

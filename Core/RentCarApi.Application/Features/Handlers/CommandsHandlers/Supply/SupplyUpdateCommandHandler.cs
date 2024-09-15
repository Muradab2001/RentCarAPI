using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Color.Update;
using RentCarApi.Application.Features.Commands.Model.Update;
using RentCarApi.Application.Features.Commands.Supply.Update;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Supply
{
    public class SupplyUpdateCommandHandler : IRequestHandler<SupplyUpdateCommandRequest, SupplyUpdateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SupplyUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplyUpdateCommandResponse> Handle(SupplyUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SupplyUpdateCommandResponse();
            var supply = await _unitOfWork.GetReadRepository<Domain.Models.Supply>().GetAsync(x => x.Id == request.Id);
            _mapper.Map(request, supply);
            _unitOfWork.GetWriteRepository<Domain.Models.Supply>().Update(supply);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}

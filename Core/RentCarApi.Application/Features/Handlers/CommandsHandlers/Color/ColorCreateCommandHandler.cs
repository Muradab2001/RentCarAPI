using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Color.Create;
using RentCarApi.Application.Features.Commands.VehicleType.Create;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Color
{
    public class ColorCreateCommandHandler : IRequestHandler<ColorCreateCommandRequest, ColorCreateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ColorCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ColorCreateCommandResponse> Handle(ColorCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ColorCreateCommandResponse();
            var color = _mapper.Map<Domain.Models.Color>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Color>().AddAsync(color);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}

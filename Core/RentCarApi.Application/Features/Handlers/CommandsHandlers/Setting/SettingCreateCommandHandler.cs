using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Review.Create;
using RentCarApi.Application.Features.Commands.Setting.Create;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Common;
using RentCarApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Setting
{
    public class SettingCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SettingCreateCommandRequest, SettingCreateCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<SettingCreateCommandResponse> Handle(SettingCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SettingCreateCommandResponse();
            var existed = await _unitOfWork.GetReadRepository<Domain.Common.Setting>().GetAsync(c => c.Key == request.Key);
            if (existed is not null) throw new Exception();
            var setting = _mapper.Map<Domain.Common.Setting>(request);
            await _unitOfWork.GetWriteRepository<Domain.Common.Setting>().AddAsync(setting);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
    }
}

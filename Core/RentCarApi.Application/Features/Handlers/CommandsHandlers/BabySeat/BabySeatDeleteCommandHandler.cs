using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.BabySeat.Delete;
using RentCarApi.Application.Features.Commands.Brand.Delete;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.BabySeat
{
    public class BabySeatDeleteCommandHandler(IStorageService storageService, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<BabySeatDeleteCommandRequest, BabySeatDeleteCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStorageService _storageService = storageService;

        public async Task<BabySeatDeleteCommandResponse> Handle(BabySeatDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BabySeatDeleteCommandResponse();
            var babySeat = await _unitOfWork.GetReadRepository<Domain.Models.BabySeat>().GetAsync(x => x.Id == request.Id, true, b => b.Include(b => b.Images));
            if (babySeat == null) response.Message = "Not found";
            else
            {
                foreach (var item in babySeat.Images)
                {
                    await _storageService.DeletePhotoAsync(item.ImageUrl);
                }
                await _unitOfWork.GetWriteRepository<Domain.Models.BabySeat>().RemoveAsync(babySeat);
                if (await _unitOfWork.SaveChangesAsync() < 1) response.Message = "Something went Wrong";
                response.Id = request.Id;
            }
            return response;
        }
    }
}

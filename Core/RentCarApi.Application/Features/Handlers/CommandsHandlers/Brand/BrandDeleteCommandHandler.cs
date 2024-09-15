using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Brand.Delete;
using RentCarApi.Application.Features.Commands.VehicleType.Delete;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Brand
{
    public class BrandDeleteCommandHandler : IRequestHandler<BrandDeleteCommandRequest, BrandDeleteCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;

        public BrandDeleteCommandHandler(IStorageService storageService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _storageService = storageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandDeleteCommandResponse> Handle(BrandDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BrandDeleteCommandResponse();
            var brand = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAsync(v => v.Id == request.Id);
            if (brand == null) response.Message = "Not found";
            else
            {
                await _storageService.DeletePhotoAsync(brand.Image);
                await _unitOfWork.GetWriteRepository<Domain.Models.Brand>().RemoveAsync(brand);
                if (await _unitOfWork.SaveChangesAsync() < 1) response.Message = "Something went Wrong";
                response.Id = request.Id;
            }
            return response;
        }
    }
}

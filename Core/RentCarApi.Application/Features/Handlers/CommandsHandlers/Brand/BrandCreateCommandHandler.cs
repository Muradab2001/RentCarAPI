using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Brand.Create;
using RentCarApi.Application.Features.Commands.Color.Create;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Brand
{
    public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommandRequest, BrandCreateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;
        public BrandCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IStorageService storageService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }
        public async Task<BrandCreateCommandResponse> Handle(BrandCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BrandCreateCommandResponse();
            var brand = _mapper.Map<Domain.Models.Brand>(request);
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.GetWriteRepository<Domain.Models.Brand>().AddAsync(brand);
            brand.Image=await _storageService.UploadPhotoAsync(request.Image,Domain.Enum.FolderNames.Brand);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                await _unitOfWork.RollbackTransactionAsync();
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            await _unitOfWork.CommitTransactionAsync();
            return response;
        }
    }
}

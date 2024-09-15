using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Brand.Update;
using RentCarApi.Application.Features.Commands.VehicleType.Update;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Brand
{
    public class BrandUpdateCommandHandler : IRequestHandler<BrandUpdateCommandRequest, BrandUpdateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;

        public BrandUpdateCommandHandler(IStorageService storageService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _storageService = storageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandUpdateCommandResponse> Handle(BrandUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BrandUpdateCommandResponse();
            var oldBrand= await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAsync(b=>b.Id==request.Id,tracking:false);
            if (oldBrand is null) { response.Message = "Something went wrong!"; }
            var brand = _mapper.Map<Domain.Models.Brand>(request);
            if (request.Image != null)
            {
                if (!FileHelpers.ImageIsOkay(request.Image, 3))
                {
                    response.Succeeded = false;
                    response.Message = "Invalid image file or the file size exceeds the limit of 3 MB.";
                    return response;
                }
                brand.Image = await _storageService.UploadPhotoAsync(request.Image, Domain.Enum.FolderNames.Brand);
                await _storageService.DeletePhotoAsync(oldBrand.Image);
            }
            brand.Image=oldBrand.Image;
            var isSuccess = _unitOfWork.GetWriteRepository<Domain.Models.Brand>().Update(brand);
            if (await _unitOfWork.SaveChangesAsync() < 1 || !isSuccess)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}

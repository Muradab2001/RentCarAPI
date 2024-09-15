using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.BabySeat.Update;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Exceptions;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.BabySeat
{
    public class BabySeatUpdateCommandHandler(IUnitOfWork unitOfWork, IStorageService storageService, IMapper mapper)
        : IRequestHandler<BabySeatUpdateCommandRequest, BabySeatUpdateCommandResponse>
    {
        private readonly IStorageService _storageService = storageService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BabySeatUpdateCommandResponse> Handle(BabySeatUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BabySeatUpdateCommandResponse();
            var existingBabySeat = await _unitOfWork.GetReadRepository<Domain.Models.BabySeat>()
                .GetAsync(x => x.Id == request.Id, true, query => query.Include(m => m.Images).Include(m=>m.CompanyData))
                ?? throw new EntityNotFoundException("Baby seat with this id does not exist!");
            var babySeat = _mapper.Map<Domain.Models.BabySeat>(request);
            babySeat.CompanyDataId = existingBabySeat.CompanyDataId;

            foreach (var imageId in request.ImagesToDelete)
            {
                if (imageId != 0)
                {
                    Image<Domain.Models.BabySeat>? img = await _unitOfWork.GetReadRepository<Image<Domain.Models.BabySeat>>().GetAsync(x => x.Id == imageId, false);
                    if (img is null)
                    {
                        throw new InvalidFileFormatException();
                    }
                    else
                    {
                        await _storageService.DeletePhotoAsync(img.ImageUrl);
                        await _unitOfWork.GetWriteRepository<Image<Domain.Models.BabySeat>>().RemoveAsync(img);
                    }
                }
            }

            foreach (var image in request.ImagesToAdd)
            {
                var img = new Image<Domain.Models.BabySeat>()
                {
                    ImageUrl = await _storageService.UploadPhotoAsync(image, FolderNames.BabySeat),
                    Item = babySeat,
                    ItemId = babySeat.Id
                };
                await _unitOfWork.GetWriteRepository<Image<Domain.Models.BabySeat>>().AddAsync(img);
            }

            _unitOfWork.GetWriteRepository<Domain.Models.BabySeat>().Update(babySeat);

            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Message = ResponseMessages.Failed;
                response.Succeeded = false;
            }
            return response;
        }
    }
}
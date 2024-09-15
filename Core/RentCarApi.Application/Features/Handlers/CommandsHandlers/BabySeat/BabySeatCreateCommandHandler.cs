using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.BabySeat.Create;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.BabySeat;
public class BabySeatCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IStorageService storageService) : IRequestHandler<BabySeatCreateCommandRequest, BabySeatCreateCommandResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStorageService _storageService = storageService;

    public async Task<BabySeatCreateCommandResponse> Handle(BabySeatCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new BabySeatCreateCommandResponse();
        var companyData = await _unitOfWork.GetReadRepository<CompanyData>().GetAsync(cd => cd.AppUserId == request.AppUserId);
        if (companyData is not null)
        {
            var babySeatInDb = await _unitOfWork.GetReadRepository<Domain.Models.BabySeat>()
                                                .GetAsync(bs => bs.CompanyDataId == companyData.Id);
            if (babySeatInDb is not null)
            {
                response.Succeeded = false;
                response.Message = "BabySeat Already Exist!";
                return response;
            }
            var babySeat = _mapper.Map<Domain.Models.BabySeat>(request);
            babySeat.CompanyDataId = companyData.Id;
            foreach (var image in request.Images)
            {
                var img = new Image<Domain.Models.BabySeat>()
                {
                    ImageUrl = await _storageService.UploadPhotoAsync(image, Domain.Enum.FolderNames.BabySeat),
                    Item = babySeat,
                    ItemId = babySeat.Id
                };
                await _unitOfWork.GetWriteRepository<Image<Domain.Models.BabySeat>>().AddAsync(img);
            }
            await _unitOfWork.GetWriteRepository<Domain.Models.BabySeat>()
                             .AddAsync(babySeat);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = ResponseMessages.Failed;
            }
        }
        else
        {
            response.Succeeded = false;
            response.Message = ResponseMessages.NotFound;
        }
        return response;
    }
}
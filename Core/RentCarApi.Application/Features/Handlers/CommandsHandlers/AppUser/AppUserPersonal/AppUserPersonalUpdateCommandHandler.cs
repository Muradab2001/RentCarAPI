using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.Update;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.AppUserPersonal;
public class AppUserPersonalUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService)
       : IRequestHandler<AppUserPersonalUpdateCommandRequest, AppUserPersonalUpdateCommandResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStorageService _storageService = storageService;

    public async Task<AppUserPersonalUpdateCommandResponse> Handle(AppUserPersonalUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new AppUserPersonalUpdateCommandResponse();
        var location = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetSingleAsync(l => l.Id == request.LocationId)
                ?? throw new EntityNotFoundException("Location Not Found!");
        var appUser = await _unitOfWork.GetReadRepository<Domain.Models.AppUser>().GetAsync(a => a.Id == request.AppUserId, include: query => query.Include(au => au.PersonalData))
                ?? throw new EntityNotFoundException("User Not Found!");
        var personalData = _mapper.Map<Domain.Models.PersonalData>(request.PersonalData);
        personalData.Id = appUser.PersonalData.Id;
        personalData.IdCardImage = appUser.PersonalData.IdCardImage;
        personalData.DrivingLicenseImage = appUser.PersonalData.DrivingLicenseImage;
        if (request.PersonalData.IdCardImage is not null)
        {
            personalData.IdCardImage = await _storageService.UploadPhotoAsync(request.PersonalData.IdCardImage, FolderNames.PersonalData);
            
        }
        if (request.PersonalData.DrivingLicenseImage is not null)
        {
            personalData.DrivingLicenseImage = await _storageService.UploadPhotoAsync(request.PersonalData.DrivingLicenseImage, FolderNames.PersonalData);
            
        }
        string oldIdCardImageUrl = appUser.PersonalData.IdCardImage;
        string oldDrivingLicenseImageUrl = appUser.PersonalData.DrivingLicenseImage;
        
        appUser.PersonalData = personalData;
        appUser.LocationId = request.LocationId;
        _unitOfWork.GetWriteRepository<Domain.Models.AppUser>().Update(appUser);
        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            response.Succeeded = false;
            response.Message = ResponseMessages.Failed;
        }
        else 
        {
            await _storageService.DeletePhotoAsync(oldIdCardImageUrl);
            await _storageService.DeletePhotoAsync(oldDrivingLicenseImageUrl);
        }
        return response;
    }
}

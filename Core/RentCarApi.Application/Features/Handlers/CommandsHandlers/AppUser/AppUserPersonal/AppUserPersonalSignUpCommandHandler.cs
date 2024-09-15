using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.SignUp;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Services;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.AppUserPersonal;
public class AppUserPersonalSignUpCommandHandler : IRequestHandler<AppUserPersonalSignUpCommandRequest, AppUserPersonalSignUpCommandResponse>
{
    private readonly UserManager<Domain.Models.AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;
    public AppUserPersonalSignUpCommandHandler(UserManager<Domain.Models.AppUser> userManager, IMapper mapper, IStorageService storageService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _storageService = storageService;
    }
    public async Task<AppUserPersonalSignUpCommandResponse> Handle(AppUserPersonalSignUpCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new AppUserPersonalSignUpCommandResponse();
        if (request.PersonalData.IdCardImage.ImageIsOkay(2) && request.PersonalData.DrivingLicenseImage.ImageIsOkay(2))
        {
            var appUser = _mapper.Map<Domain.Models.AppUser>(request);
            var idCardİmageUrl = await _storageService.UploadPhotoAsync(request.PersonalData.IdCardImage, FolderNames.PersonalData);
            var drivingLicenseImageUrl = await _storageService.UploadPhotoAsync(request.PersonalData.DrivingLicenseImage, FolderNames.PersonalData);
            appUser.PersonalData.IdCardImage = idCardİmageUrl;
            appUser.PersonalData.DrivingLicenseImage = drivingLicenseImageUrl;
            var result = await _userManager.CreateAsync(appUser, request.Password);

            response.Succeeded = result.Succeeded;
            response.Message = result.Succeeded ? "User created successfully." : $"User creation failed: {string.Join("; ", result.Errors.Select(e => e.Description))}";
        }
        else
        {
            throw new InvalidFileFormatException();
        }
        return response;
    }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.SignUp;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.AppUserCompany;

public class AppUserCompanySignUpCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IStorageService storageService, UserManager<Domain.Models.AppUser> userManager)
    : IRequestHandler<AppUserCompanySignUpCommandRequest, AppUserCompanySignUpCommandResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStorageService _storageService = storageService;
    private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
    public async Task<AppUserCompanySignUpCommandResponse> Handle(AppUserCompanySignUpCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new AppUserCompanySignUpCommandResponse();
        if (request.CompanyData.Image.ImageIsOkay(3))
        {
            var company = _mapper.Map<Domain.Models.AppUser>(request);
            var result = await _userManager.CreateAsync(company, request.Password);
            await _userManager.AddToRoleAsync(company, "Company");
            company.CompanyData.Image = await _storageService.UploadPhotoAsync(request.CompanyData.Image, FolderNames.CompanyData);
            response.IsSuccess = result.Succeeded;
            response.Message = result.Succeeded ? "User created successfully." : $"User creation failed: {string.Join("; ", result.Errors.Select(e => e.Description))}";
        }
        else
        {
            throw new InvalidFileFormatException();
        }

        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            throw new DomainException("Something went Wrong");
        }
        return response;
    }
}
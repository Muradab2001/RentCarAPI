using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Update;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUserCompany
{
    public class AppUserCompanyUpdateCommandHandler(IStorageService storageService, IUnitOfWork unitOfWork, IMapper mapper) :
        IRequestHandler<AppUserCompanyUpdateCommandRequest, AppUserCompanyUpdateCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStorageService _storageService = storageService;

        public async Task<AppUserCompanyUpdateCommandResponse> Handle(AppUserCompanyUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AppUserCompanyUpdateCommandResponse();
            var location = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetSingleAsync(l => l.Id == request.LocationId) 
                ?? throw new EntityNotFoundException("Location not found");

            var appUser = await _unitOfWork.GetReadRepository<Domain.Models.AppUser>()
               .GetAsync(x => x.Id == request.AppUserId, true, query => query.Include(x => x.CompanyData)) 
               ?? throw new EntityNotFoundException("User not found");

            var companyData = _mapper.Map<Domain.Models.CompanyData>(request.CompanyData);
            var imageUrl = appUser.CompanyData.Image;

            if (request.CompanyData.Image != null)
            {
                imageUrl = await _storageService.UploadPhotoAsync(request.CompanyData.Image, FolderNames.CompanyData);
                await _storageService.DeletePhotoAsync(appUser.CompanyData.Image);
            }
            appUser.SetDetails(request.LocationId, companyData.Name, imageUrl);
            _unitOfWork.GetWriteRepository<Domain.Models.AppUser>().Update(appUser);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                throw new DomainException("Something went Wrong");
            }
            return response;
        }
    }
}
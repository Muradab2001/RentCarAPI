using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Delete;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.AppUserCompany
{
    public class AppUserCompanyDeleteCommandHandler(IUnitOfWork unitOfWork, IStorageService storageService, UserManager<Domain.Models.AppUser> userManager)
        : IRequestHandler<AppUserCompanyDeleteCommandRequest, AppUserCompanyDeleteCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
        private readonly IStorageService _storageService = storageService;
        public async Task<AppUserCompanyDeleteCommandResponse> Handle(AppUserCompanyDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AppUserCompanyDeleteCommandResponse();
            var user = await _userManager.Users.Include(m => m.CompanyData).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (user != null)
            {
                await _storageService.DeletePhotoAsync(user.CompanyData.Image);
                await _userManager.DeleteAsync(user);
                if (await _unitOfWork.SaveChangesAsync() < 1)
                {
                    throw new DomainException("Something went Wrong");
                }
            }
            else
            {
                throw new UserNotFoundException("User do not found!");
            }
            return response;
        }
    }
}
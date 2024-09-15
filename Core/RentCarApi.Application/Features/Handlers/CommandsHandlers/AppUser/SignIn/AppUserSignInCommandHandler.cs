using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Common.Interfaces.Authentication;
using RentCarApi.Application.Features.Commands.AppUser.SignIn;
using RentCarApi.Application.Features.Response;
using RentCarApi.Domain.Exceptions;
using System.Security.Claims;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.SignIn;
public class AppUserPersonalSignInCommandHandler(UserManager<Domain.Models.AppUser> userManager, SignInManager<Domain.Models.AppUser> signInManager,
 ITokenGeneratorService tokenGeneratorService)
 : IRequestHandler<AppUserSignInCommandRequest, AppUserSignInCommandResponse>
{
    private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
    private readonly SignInManager<Domain.Models.AppUser> _signInManager = signInManager;
    private readonly ITokenGeneratorService _tokenGeneratorService = tokenGeneratorService;
    public async Task<AppUserSignInCommandResponse> Handle(AppUserSignInCommandRequest request, CancellationToken cancellationToken)
    {
        var username = request.Username;
        var user = await _userManager.FindByNameAsync(username)
            ?? throw new UserNotFoundException($"User with {username} not found!");
        SignInResult result =
            await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
        if (!result.Succeeded)
        {
            throw new VisibleExceptions("Verify not confirmed or password incorrect");
        }
        if (result.IsLockedOut)
        {
            throw new VisibleExceptions("Lock! 5 minutes block");
        }
        List<Claim> claims = await _tokenGeneratorService.CreateClaims(user);

        return new AppUserSignInCommandResponse
        {
            IsSuccess = true,
            Message = ResponseMessages.Success,
            RefreshToken = await _tokenGeneratorService.GenerateRefreshTokenAsync(claims, user.Id),
            AccessToken = await _tokenGeneratorService.GenerateJwtAccessTokenAsync(claims)
        };
    }
}
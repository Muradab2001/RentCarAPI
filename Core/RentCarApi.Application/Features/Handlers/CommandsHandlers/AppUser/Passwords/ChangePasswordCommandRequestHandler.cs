using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands;
using System.Security.Claims;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.Passwords;
public class ChangePasswordCommandHandler(UserManager<Domain.Models.AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<ChangePasswordCommandRequest, ChangePasswordCommandResponse>
{
    private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        // Get the current user's claims principal
        var userPrincipal = _httpContextAccessor.HttpContext.User;

        // Ensure the user is authenticated
        if (!userPrincipal.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        // Get the user's ID from claims
        var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != request.AppUserId.ToString())
        {
            throw new UnauthorizedAccessException("User ID does not match the authenticated user.");
        }

        var user = await _userManager.FindByIdAsync(request.AppUserId.ToString()) ?? throw new Exception("User not found.");

        if (request.NewPasswordConfirm != request.NewPassword)
        {
            throw new Exception("New password and confirmation password do not match.");
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to change password.");
        }
        return new ChangePasswordCommandResponse { IsSuccess = true };
    }
}
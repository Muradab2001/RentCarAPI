using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.Passwords.ForgotPassword
{
    public class OtpConfirmationCommandHandler(UserManager<Domain.Models.AppUser> userManager)
        : IRequestHandler<OtpConfirmationCommandRequest, OtpConfirmationCommandResponse>
    {
        private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
        public async Task<OtpConfirmationCommandResponse> Handle(OtpConfirmationCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new InvalidEmailAddressException();

            if (user.OtpExpiration > DateTime.UtcNow.AddMinutes(5))
            {
                throw new DomainException("Otp code expired");
            }
            await _userManager.UpdateAsync(user);
            return new OtpConfirmationCommandResponse()
            {
                IsSuccess = true,
                Message = "OTP code confirmed successfully. You can now proceed to reset your password."
            };
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.Passwords.ForgotPassword
{
    public class PasswordConfirmationCommandHandler(UserManager<Domain.Models.AppUser> userManager)
        : IRequestHandler<PasswordConfirmationCommandRequest, PasswordConfirmationCommandResponse>
    {
        private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
        public async Task<PasswordConfirmationCommandResponse> Handle(PasswordConfirmationCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new InvalidEmailAddressException();
            if (user.Otp is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
                if (result.Succeeded)
                {
                    user.Otp = null;
                    user.OtpExpiration = null;
                    await _userManager.UpdateAsync(user);

                    return new PasswordConfirmationCommandResponse
                    {
                        IsSuccess = true,
                        Message = "Password has been successfully reset."
                    };
                }
                else
                {
                    return new PasswordConfirmationCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Error occurred while resetting the password: " + string.Join(", ", result.Errors)
                    };
                }
            }
            else
            {
                return new PasswordConfirmationCommandResponse
                {
                    IsSuccess = false,
                    Message = "OTP is not valid or has expired."
                };
            }
        }
    }
}
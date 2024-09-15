using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Common.Interfaces.Email;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;
using RentCarApi.Application.Helpers;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.AppUser.Passwords.ForgotPassword
{
    public class SendEmailCommandHandler(UserManager<Domain.Models.AppUser> userManager, IEmailManager emailManager)
        : IRequestHandler<SendEmailCommandRequest, SendEmailCommandResponse>
    {
        private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
        private readonly IEmailManager _emailManager = emailManager;
        public async Task<SendEmailCommandResponse> Handle(SendEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new Exception("Provided email is not associated with any account");

            string otpCode = GenerateUniqueOtp();
            var email = new Email()
            {
                To = request.Email,
                Subject = "Password Reset Request",
                Body = $"<html><body><p><strong>Dear User,</strong></p><br><p>You requested a password reset. Use the following code to reset your password:<br><strong>{otpCode}</strong></p><br><p>Note: Do not share this code with anyone.</p><p>If you encounter any issues, please contact the system administrator.</p><br><p>Best regards,</p><p>Temho Pool</p></body></html>"
            };

            user.SetOtp(otpCode); // Assuming you have extended the IdentityUser to include this method
            await _userManager.UpdateAsync(user);

            await _emailManager.SendEmail(email);
            return new SendEmailCommandResponse();
        }

        private string GenerateUniqueOtp()
        {
            string otpCode;
            do
            {
                otpCode = new Random().Next(100000, 999999).ToString();
            }
            while (_userManager.Users.Any(x => x.Otp == otpCode));

            return otpCode;
        }
    }
}
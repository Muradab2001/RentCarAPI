using MediatR;

namespace RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword
{
    public class OtpConfirmationCommandRequest : IRequest<OtpConfirmationCommandResponse>
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
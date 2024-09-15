using MediatR;

namespace RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword
{
    public class PasswordConfirmationCommandRequest : IRequest<PasswordConfirmationCommandResponse>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
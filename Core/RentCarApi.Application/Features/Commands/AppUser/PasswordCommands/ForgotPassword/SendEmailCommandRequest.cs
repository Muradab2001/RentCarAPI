using MediatR;

namespace RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword
{
    public class SendEmailCommandRequest : IRequest<SendEmailCommandResponse>
    {
        public string Email { get; set; }
    }
}
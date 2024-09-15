using MediatR;

namespace RentCarApi.Application.Features.Commands.AppUser.SignIn
{
    public class AppUserSignInCommandRequest : IRequest<AppUserSignInCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
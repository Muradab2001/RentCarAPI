using MediatR;

namespace RentCarApi.Application.Features.Commands.AppUser.PasswordCommands
{
    public class ChangePasswordCommandRequest : IRequest<ChangePasswordCommandResponse>
    {
        public int AppUserId {  get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
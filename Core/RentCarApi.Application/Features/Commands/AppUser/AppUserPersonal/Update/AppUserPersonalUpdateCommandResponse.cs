using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.Update;
public class AppUserPersonalUpdateCommandResponse
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}

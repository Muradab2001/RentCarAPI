using MediatR;
using RentCarApi.Application.DTOs;
namespace RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.Update;
public class AppUserPersonalUpdateCommandRequest : IRequest<AppUserPersonalUpdateCommandResponse>
{
    public int AppUserId {  get; set; }
    public PersonalDataDTO PersonalData { get; set; }
    public int LocationId { get; set; }
}
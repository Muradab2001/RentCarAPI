using MediatR;
using RentCarApi.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.SignUp;
public class AppUserPersonalSignUpCommandRequest : IRequest<AppUserPersonalSignUpCommandResponse>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    public string RepeatPassword { get; set; }
    public PersonalDataDTO PersonalData { get; set; }
    public int LocationId { get; set; }
}
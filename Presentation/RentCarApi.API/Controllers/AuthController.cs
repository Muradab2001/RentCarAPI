using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.SignUp;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.SignUp;
using RentCarApi.Application.Features.Commands.AppUser.SignIn;
using RentCarApi.Application.Features.Commands.GenerateRefreshToken.Create;

namespace RentCarApi.API.Controllers;
public class AuthController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] AppUserSignInCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> SignUpPersonal([FromForm] AppUserPersonalSignUpCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> SignUpCompany([FromForm] AppUserCompanySignUpCommandRequest request)
    {
        await _mediator.Send(request);
        return Created();
    }

    [HttpPatch]
    public async Task<IActionResult> RefreshToken(RefreshTokenCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
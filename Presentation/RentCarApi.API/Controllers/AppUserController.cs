using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Delete;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Update;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.Update;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands;
using RentCarApi.Application.Features.Commands.AppUser.PasswordCommands.ForgotPassword;

namespace RentCarApi.API.Controllers
{
    public class AppUserController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromForm] AppUserCompanyUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersonalUser([FromForm] AppUserPersonalUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("email-step1")]
        public async Task<IActionResult> OtpConfirmation([FromBody] SendEmailCommandRequest command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "Operation completed successfully!" });
        }

        [HttpPost("otp-step2")]
        public async Task<IActionResult> OtpConfirmation([FromBody] OtpConfirmationCommandRequest command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "Otp sent successfully!" });
        }

        [HttpPut("passwordConfirmation-step3")]
        public async Task<IActionResult> ForgotPassword([FromBody] PasswordConfirmationCommandRequest command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "Password changed successfully!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] AppUserCompanyDeleteCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
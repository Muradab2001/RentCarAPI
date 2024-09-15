using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Review.Create;
using RentCarApi.Application.Features.Commands.Setting.Create;
using RentCarApi.SignalR.Hubs;

namespace RentCarApi.API.Controllers
{
    public class SettingController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(SettingCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

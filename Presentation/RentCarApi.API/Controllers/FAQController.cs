using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Color.Create;
using RentCarApi.Application.Features.Commands.FAQ.Create;

namespace RentCarApi.API.Controllers
{
    public class FAQController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(FAQCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

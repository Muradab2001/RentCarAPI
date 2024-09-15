using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Color.Create;
using RentCarApi.Application.Features.Commands.Color.Delete;
using RentCarApi.Application.Features.Commands.Color.Update;
using RentCarApi.Application.Features.Queries.Color;

namespace RentCarApi.API.Controllers
{
    [ApiController]
    public class ColorController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new ColorGetAllQueryRequest());
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] ColorGetByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ColorUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ColorDeleteCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
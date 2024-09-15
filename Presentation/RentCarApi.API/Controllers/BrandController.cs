using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Brand.Create;
using RentCarApi.Application.Features.Commands.Brand.Delete;
using RentCarApi.Application.Features.Commands.Brand.Update;
using RentCarApi.Application.Features.Queries.Brend;

namespace RentCarApi.API.Controllers
{
    public class BrandController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new BrandGetAllQueryRequest());
            return Ok(response);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] BrandGetByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(BrandUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(BrandDeleteCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

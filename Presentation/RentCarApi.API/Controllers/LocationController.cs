using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Location.Create;
using RentCarApi.Application.Features.Commands.Location.Delete;
using RentCarApi.Application.Features.Commands.Location.Update;
using RentCarApi.Application.Features.Queries.Location.GetAll;

namespace RentCarApi.API.Controllers
{
    //[Authorize(Roles = "Company")]
    public class LocationController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new LocationGetAllQueryRequest());
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] LocationGetByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(LocationUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(LocationDeleteCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
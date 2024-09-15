using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.VehicleType.Create;
using RentCarApi.Application.Features.Commands.VehicleType.Delete;
using RentCarApi.Application.Features.Commands.VehicleType.Update;
using RentCarApi.Application.Features.Queries.VehicleType;

namespace RentCarApi.API.Controllers
{
    public class VehicleTypeController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new VehicleTypeGetAllQueryRequest());
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] VehicleTypeGetByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleTypeCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(VehicleTypeUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(VehicleTypeDeleteCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
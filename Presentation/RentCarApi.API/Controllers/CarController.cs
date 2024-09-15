using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Car.Create;
using RentCarApi.Application.Features.Commands.Car.Delete;
using RentCarApi.Application.Features.Commands.Car.Update;
using RentCarApi.Application.Features.Queries.Car.GetAll;
using RentCarApi.Application.Features.Queries.Car.GetById;

namespace RentCarApi.API.Controllers;
public class CarController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CarGetAllQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] CarGetByIdQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CarCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] CarUpdateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
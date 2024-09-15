using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.BabySeat.Create;
using RentCarApi.Application.Features.Commands.BabySeat.Delete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using RentCarApi.Application.Features.Commands.BabySeat.Update;
using RentCarApi.Application.Features.Queries.BabySeat;

namespace RentCarApi.API.Controllers;
public class BabySeatController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] BabySeatGetByIdQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] BabySeatCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] BabySeatUpdateCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok(new { Message = "Baby Seat Updated Successfully!" });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(BabySeatDeleteCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
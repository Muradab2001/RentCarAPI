using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Supply.Create;
using RentCarApi.Application.Features.Commands.Supply.Delete;
using RentCarApi.Application.Features.Commands.Supply.Update;
using RentCarApi.Application.Features.Queries.Supply.GetAll;
using RentCarApi.Application.Features.Queries.Supply.GetById;

namespace RentCarApi.API.Controllers;

public class SupplyController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new SupplyGetAllQueryRequest());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] SupplyGetByIdQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SupplyCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplyUpdateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(SupplyDeleteCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
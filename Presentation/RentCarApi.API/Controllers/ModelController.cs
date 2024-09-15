using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Model.Create;
using RentCarApi.Application.Features.Commands.Model.Delete;
using RentCarApi.Application.Features.Commands.Model.Update;
using RentCarApi.Application.Features.Queries.Model;

namespace RentCarApi.API.Controllers;
public class ModelController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new ModelGetAllQueryRequest());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] ModelGetByIdQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ModelCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ModelUpdateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(ModelDeleteCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
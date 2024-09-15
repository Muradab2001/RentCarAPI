using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Order.Create;

namespace RentCarApi.API.Controllers;
public class OrderController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] OrderCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
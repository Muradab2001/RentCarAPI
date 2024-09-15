using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Review.Create;
using RentCarApi.Application.Features.Commands.Review.Delete;
using RentCarApi.Application.Features.Queries.Review.GetAll;

namespace RentCarApi.API.Controllers;
public class ReviewController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new ReviewGetAllQueryRequest());
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReviewCreateCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] ReviewDeleteCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
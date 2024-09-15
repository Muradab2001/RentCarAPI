using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.PromoCode.Create;

namespace RentCarApi.API.Controllers;
public class PromoCodeController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;
    [HttpPost]
    public async Task<IActionResult> Create(PromoCodeCreateCommandRequest request) 
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}

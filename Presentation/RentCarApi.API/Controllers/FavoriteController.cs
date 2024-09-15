using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCarApi.API.Controllers.v1.Base;
using RentCarApi.Application.Features.Commands.Favorite.Create;
using RentCarApi.Application.Features.Commands.Favorite.Delete;
using RentCarApi.Application.Features.Queries.Favorite;

namespace RentCarApi.API.Controllers
{
    public class FavoriteController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<GetFavoritesByUserIdResponse>>> GetFavoritesByUserId([FromQuery] GetFavoritesByUserIdRequest request)
        {
            var favorites = await _mediator.Send(request);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(AddToFavoriteCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromFavorites(RemoveFromFavoriteCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
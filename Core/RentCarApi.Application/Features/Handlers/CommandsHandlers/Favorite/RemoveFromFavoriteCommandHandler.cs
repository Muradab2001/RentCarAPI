using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.Favorite.Delete;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Favorite
{
    public class RemoveFromFavoriteCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<RemoveFromFavoriteCommandRequest, RemoveFromFavoriteCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<RemoveFromFavoriteCommandResponse> Handle(RemoveFromFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            var favoriteItem = await _unitOfWork.GetReadRepository<Domain.Models.Favorite>().GetAsync(
                x => x.UserId == request.UserId && x.CarId == request.CarId, true, query=>query.Include(m=>m.Car).Include(x=>x.User));

            if (favoriteItem != null)
            {
                await _unitOfWork.GetWriteRepository<Domain.Models.Favorite>().RemoveAsync(favoriteItem);
                await _unitOfWork.SaveChangesAsync();
                return new RemoveFromFavoriteCommandResponse { IsSuccess = true, Message = "Product removed successfully" };
            }
            else
            {
                return new RemoveFromFavoriteCommandResponse { IsSuccess = false, Message = "Product not found " };
            }
        }
    }
}
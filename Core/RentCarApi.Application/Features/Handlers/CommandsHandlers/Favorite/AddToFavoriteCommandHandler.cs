using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.Favorite.Create;
using RentCarApi.Application.Features.Queries.Favorite;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Favorite
{
    public class AddToFavoriteCommandHandler(IUnitOfWork unitOfWork, UserManager<Domain.Models.AppUser> userManager, IMapper mapper)
        : IRequestHandler<AddToFavoriteCommandRequest, AddToFavoriteCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<Domain.Models.AppUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        public async Task<AddToFavoriteCommandResponse> Handle(AddToFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AddToFavoriteCommandResponse();
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return new AddToFavoriteCommandResponse { IsSuccess = false, Message = "User does not exist" };
            }
            var car = await _unitOfWork.GetReadRepository<Domain.Models.Car>().GetAsync(x => x.Id == request.CarId);

            if (car == null)
            {
                return new AddToFavoriteCommandResponse { IsSuccess = false, Message = "Car doesn't found" };
            }

            var favoriteItem = await _unitOfWork.GetReadRepository<Domain.Models.Favorite>().GetAsync(
                x => x.UserId == request.UserId && x.CarId == request.CarId);

            if (favoriteItem == null)
            {
                favoriteItem = new Domain.Models.Favorite
                {
                    UserId = request.UserId,
                    CarId = request.CarId,
                    User = user,
                    Car = car,
                };
                await _unitOfWork.GetWriteRepository<Domain.Models.Favorite>().AddAsync(favoriteItem);
            }
            else
            {
                await _unitOfWork.GetWriteRepository<Domain.Models.Favorite>().RemoveAsync(favoriteItem);
            }

            var favorites = await _unitOfWork.GetReadRepository<Domain.Models.Favorite>().GetWhere(x => x.UserId == user.Id);
            var favoriteResponses = _mapper.Map<List<FavoriteResponse>>(favorites);
            var favoriteModel = new GetFavoritesByUserIdResponse
            {
                Favorites = favoriteResponses,
            };
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}
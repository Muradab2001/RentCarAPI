using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Queries.Favorite;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Favorite
{
    public class GetFavoritesByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetFavoritesByUserIdRequest, List<GetFavoritesByUserIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<List<GetFavoritesByUserIdResponse>> Handle(GetFavoritesByUserIdRequest request, CancellationToken cancellationToken)
        {
            var favoriteItems = await _unitOfWork.GetReadRepository<Domain.Models.Favorite>()
                .GetWhere(x => x.UserId == request.UserId, true, query => query
                .Include(m => m.Car)
                .ThenInclude(m => m.Images));

            //var favoriteResponses = _mapper.Map<List<FavoriteResponse>>(favoriteItems);
            var favoriteResponses = favoriteItems.Select(fav => new FavoriteResponse
            {
                Id = fav.Id,
                Description = fav.Car.Description,
                Price = fav.Car.Price,
                Images = _mapper.Map<List<ImageResponse>>(fav.Car.Images)
            }).ToList();
            var response = new List<GetFavoritesByUserIdResponse>
            {
                new ()
                {
                    Favorites = favoriteResponses
                }
            };
            return response;
        }
    }
}
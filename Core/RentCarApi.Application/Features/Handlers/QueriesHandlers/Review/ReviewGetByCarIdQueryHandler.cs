using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Review.GetByCarId;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Review;
public class ReviewGetByCarIdQueryHandler : IRequestHandler<ReviewGetByCarIdQueryRequest, ReviewGetByCarIdQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ReviewGetByCarIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReviewGetByCarIdQueryResponse> Handle(ReviewGetByCarIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new ReviewGetByCarIdQueryResponse();
        int totalRate = 0;
        var car =await _unitOfWork.GetReadRepository<Domain.Models.Car>().GetSingleAsync(c => c.Id == request.CarId);
        if (car is null) throw new Exception("Car not found");
        var reviews = await _unitOfWork.GetReadRepository<Domain.Models.Review>().GetWhere(r => r.CarId == request.CarId);
        if (reviews.Any())
        {
            foreach (var review in reviews)
            {
                totalRate += review.Rate;
            }
            response.Rate = (byte)(totalRate / reviews.Count());
        }
        return response;
    }

}

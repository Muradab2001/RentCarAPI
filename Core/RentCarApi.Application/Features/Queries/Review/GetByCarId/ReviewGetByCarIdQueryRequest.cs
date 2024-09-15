using MediatR;

namespace RentCarApi.Application.Features.Queries.Review.GetByCarId;
public class ReviewGetByCarIdQueryRequest : IRequest<ReviewGetByCarIdQueryResponse>
{
    public int CarId {  get; set; }
}

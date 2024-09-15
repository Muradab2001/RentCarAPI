using MediatR;

namespace RentCarApi.Application.Features.Queries.Review.GetAll;
public class ReviewGetAllQueryRequest : IRequest<IList<ReviewGetAllQueryResponse>>
{
}

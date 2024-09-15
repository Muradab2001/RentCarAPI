using MediatR;

namespace RentCarApi.Application.Features.Queries.Brend
{
    public class BrandGetAllQueryRequest : IRequest<IList<BrandGetAllQueryResponse>>
    {
    }
}
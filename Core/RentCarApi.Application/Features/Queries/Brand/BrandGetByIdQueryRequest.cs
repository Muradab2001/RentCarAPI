using MediatR;

namespace RentCarApi.Application.Features.Queries.Brend
{
    public class BrandGetByIdQueryRequest : IRequest<BrandGetByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
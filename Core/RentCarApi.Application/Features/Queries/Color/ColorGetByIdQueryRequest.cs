using MediatR;

namespace RentCarApi.Application.Features.Queries.Color
{
    public class ColorGetByIdQueryRequest : IRequest<ColorGetByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
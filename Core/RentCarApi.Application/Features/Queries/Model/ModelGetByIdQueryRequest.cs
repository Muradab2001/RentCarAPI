using MediatR;

namespace RentCarApi.Application.Features.Queries.Model
{
    public class ModelGetByIdQueryRequest : IRequest<ModelGetByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
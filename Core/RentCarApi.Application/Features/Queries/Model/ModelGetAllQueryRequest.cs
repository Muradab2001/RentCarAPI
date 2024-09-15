using MediatR;

namespace RentCarApi.Application.Features.Queries.Model
{
    public class ModelGetAllQueryRequest : IRequest<IList<ModelGetAllQueryResponse>>
    {
    }
}
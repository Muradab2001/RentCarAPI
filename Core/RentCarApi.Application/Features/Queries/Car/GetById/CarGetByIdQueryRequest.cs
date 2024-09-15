using MediatR;

namespace RentCarApi.Application.Features.Queries.Car.GetById
{
    public class CarGetByIdQueryRequest : IRequest<CarGetByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
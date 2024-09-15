using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Queries.Car.GetById;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Car
{
    public class CarGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        : IRequestHandler<CarGetByIdQueryRequest, CarGetByIdQueryResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CarGetByIdQueryResponse> Handle(CarGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var car = await _unitOfWork.GetReadRepository<Domain.Models.Car>()
                .GetAsync(x => x.Id == request.Id, false, query => query
                        .Include(c => c.Location)
                        .Include(c => c.AppUser)
                        .ThenInclude(m=>m.CompanyData)
                        .Include(c => c.Model)
                        .ThenInclude(m=>m.Brand)
                        .Include(c=>c.Color)
                        .Include(c => c.VehicleType)
                        .Include(c => c.Images)
                        .Include(c => c.Supplies)
                        .ThenInclude(m=>m.Supply));
            return _mapper.Map<CarGetByIdQueryResponse>(car);
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Queries.Car.GetAll;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Car;
public class CarGetAllQueryHandler : IRequestHandler<CarGetAllQueryRequest, Pagination<CarGetAllQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CarGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Pagination<CarGetAllQueryResponse>> Handle(CarGetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.GetReadRepository<Domain.Models.Car>()
                              .GetAll(false, query => query
                              .Include(c => c.Location)
                              .Include(c => c.Model)
                              .Include(c => c.Brand)
                              .Include(c => c.VehicleType)
                              .Include(c => c.Images));

        if (!string.IsNullOrEmpty(request.Brand))
        {
            query = query.Where(c =>
                request.Brand == null ||
                c.Brand.Name.ToLower().Trim().Contains(request.Brand.ToLower().Trim()));
        }

        if (request.ModelIds?.Any() == true)
            query = query.Where(c => request.ModelIds.Contains(c.ModelId.Value));

        if (request.VehicleTypeId.HasValue)
            query = query.Where(c => c.VehicleTypeId == request.VehicleTypeId);

        if (request.MinPrice.HasValue || request.MaxPrice.HasValue)
            query = query.Where(c => (!request.MinPrice.HasValue || c.Price >= request.MinPrice) &&
                                      (!request.MaxPrice.HasValue || c.Price <= request.MaxPrice));

        if (request.FuelTypes?.Any() == true)
            query = query.Where(c => request.FuelTypes.Contains(c.FuelType));

        if (request.TransmissionTypes?.Any() == true)
            query = query.Where(c => request.TransmissionTypes.Contains(c.TransmissionType));

        if (request.SeatCounts?.Any() == true)
            query = query.Where(c => request.SeatCounts.Contains(c.SeatCount));

        if (request.MinYear.HasValue || request.MaxYear.HasValue)
            query = query.Where(c => (!request.MinYear.HasValue || c.Year >= request.MinYear) &&
                                      (!request.MaxYear.HasValue || c.Year <= request.MaxYear));

        if (request.ColorIds?.Any() == true)
            query = query.Where(c => request.ColorIds.Contains(c.ColorId.Value));

        if (request.LocationId.HasValue)
        {
            query = query.Where(c => c.LocationId == request.LocationId);
        }

        if (request.PickupDate.HasValue && request.ReturnDate.HasValue)
        {
            query = query.Where(c => !c.Orders.Any(o =>
                o.StartTime < request.ReturnDate &&
                o.EndTime > request.PickupDate
            ));
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var cars = await query.Skip((request.Page - 1) * request.Size)
                                  .Take(request.Size)
                                  .ToListAsync(cancellationToken);

        var carResponses = _mapper.Map<List<CarGetAllQueryResponse>>(cars);
        var paginatedResult = new Pagination<CarGetAllQueryResponse>(carResponses, request.Page, request.Size);
        return paginatedResult;
    }
}
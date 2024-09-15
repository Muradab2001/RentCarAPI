using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Common.Interfaces.Hubs;
using RentCarApi.Application.Features.Commands.Order.Create;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Order;
public class OrderCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IOrderHubService orderHub) : IRequestHandler<OrderCreateCommandRequest, OrderCreateCommandResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderHubService _orderHub = orderHub;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<OrderCreateCommandResponse> Handle(OrderCreateCommandRequest request, CancellationToken cancellationToken)
    {
        OrderCreateCommandResponse response = new();
        request.StartTime = request.StartTime.AddHours(4);
        request.EndTime = request.EndTime.AddHours(4);
        var car = await _unitOfWork.GetReadRepository<Domain.Models.Car>().GetAsync(l => l.Id == request.CarId, false, c => c.Include(c => c.AppUser))
                   ?? throw new EntityNotFoundException("Car Not Found!");
        var appUser = await _unitOfWork.GetReadRepository<Domain.Models.AppUser>().GetAsync(a => a.Id == request.AppUserId)
                ?? throw new EntityNotFoundException("User Not Found!");
        var orders = await _unitOfWork.GetReadRepository<Domain.Models.Order>().GetWhere(o => o.CarId == request.CarId);
        if (orders.Any())
        {
            var lastOrderOfCar = orders.OrderBy(x => x.EndTime).Last();
            if ((lastOrderOfCar is not null) && (lastOrderOfCar.EndTime >= request.EndTime))
            {
                throw new CarAlreadyRentedException();
            }
        }

        var order = _mapper.Map<Domain.Models.Order>(request);
        order.TotalAmount = CalculateTotalAmount(car, request.StartTime, request.EndTime);

        await _unitOfWork.GetWriteRepository<Domain.Models.Order>().AddAsync(order);
        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            response.Succeeded = false;
            response.Message = ResponseMessages.Failed;
        }
        await _orderHub.OrderAddedMessageAsync(order.Id, car.AppUser.Id);
        return response;
    }

    private double CalculateTotalAmount(Domain.Models.Car car, DateTime StartTime, DateTime EndTime)
    {
        var timeDifference = (EndTime - StartTime).Days;
        return timeDifference * car.Price;
    }
}
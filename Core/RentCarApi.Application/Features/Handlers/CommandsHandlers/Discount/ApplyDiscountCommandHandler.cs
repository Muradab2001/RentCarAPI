using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.Discount.Apply;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Exceptions;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Discount
{
    public class ApplyDiscountCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<ApplyDiscountCommandRequest, ApplyDiscountCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApplyDiscountCommandResponse> Handle(ApplyDiscountCommandRequest request, CancellationToken cancellationToken)
        {
            var discount = await _unitOfWork.GetReadRepository<Domain.Models.Discount>().GetAsync(x => x.Id == request.DiscountId, false, query => query.Include(m => m.User));
            if (discount == null || discount.UserId != request.UserId)
            {
                return new ApplyDiscountCommandResponse { Succeeded = false, Message = "Discount not found or user not authorized." };
            }
            var carsToUpdate = await _unitOfWork.GetReadRepository<Domain.Models.Car>().Table
                .Include(c => c.CarDiscounts)
                .ThenInclude(cd => cd.Discount)
                .Where(c => request.CarIds.Contains(c.Id) && c.AppUserId == request.UserId)
                .ToListAsync();

            foreach (var car in carsToUpdate)
            {
                if (!car.CarDiscounts.Any(cd => cd.DiscountId == discount.Id))
                {
                    var carDiscount = new CarDiscount
                    {
                        CarId = car.Id,
                        DiscountId = discount.Id,
                    };
                    await _unitOfWork.GetWriteRepository<CarDiscount>().AddAsync(carDiscount);
                }
                await _unitOfWork.SaveChangesAsync();
                UpdateDiscountPrice(car, request.DiscountId);
                _unitOfWork.GetWriteRepository<Domain.Models.Car>().Update(car);
            }

            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                throw new DomainException(ResponseMessages.Failed);
            }

            return new ApplyDiscountCommandResponse
            {
                Succeeded = true,
                Message = "Discount applied successfully."
            };
        }
        public async void UpdateDiscountPrice(Domain.Models.Car car, int discountId)
        {
            var carDiscounts = await _unitOfWork.GetReadRepository<CarDiscount>()
                .Table.Include(m => m.Car).Include(m => m.Discount).ToListAsync();
            if (carDiscounts == null)
            {
                car.DiscountPrice = car.Price;
                return;
            }

            var currentDiscount = carDiscounts
                                  .Where(cd => cd.Discount != null)
                                  .Select(cd => cd.Discount)
                                  .FirstOrDefault(d => d.Id == discountId);

            if (currentDiscount == null)
            {
                car.DiscountPrice = car.Price;
            }
            else
            {
                car.DiscountPrice = car.Price * (1 - currentDiscount.DiscountPercentage / 100);
            }
        }
    }
}
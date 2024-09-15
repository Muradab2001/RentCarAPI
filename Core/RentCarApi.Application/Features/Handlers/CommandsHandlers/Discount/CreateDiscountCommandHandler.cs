using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.Discount.Create;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.Repositories;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Discount
{
    public class CreateDiscountCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<CreateDiscountCommandRequest, CreateDiscountCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateDiscountCommandResponse> Handle(CreateDiscountCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetReadRepository<Domain.Models.AppUser>().GetAsync(x => x.Id == request.UserId)
                ?? throw new UserNotFoundException("User not found with this id");

            var discount = new Domain.Models.Discount
            {
                UserId = request.UserId,
                DiscountPercentage = request.DiscountPercentage,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.GetWriteRepository<Domain.Models.Discount>().AddAsync(discount);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return new CreateDiscountCommandResponse
            {
                Succeeded = saveResult > 0,
                Message = saveResult > 0 ? "Discount created successfully" : "Something went wrong!",
                DiscountId = discount.Id
            };
        }
    }
}
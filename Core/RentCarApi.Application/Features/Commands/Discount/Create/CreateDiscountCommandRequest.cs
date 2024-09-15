using MediatR;

namespace RentCarApi.Application.Features.Commands.Discount.Create
{
    public class CreateDiscountCommandRequest : IRequest<CreateDiscountCommandResponse>
    {
        public int UserId { get; set; }
        public double DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
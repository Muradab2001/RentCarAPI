using MediatR;

namespace RentCarApi.Application.Features.Commands.Discount.Apply
{
    public class ApplyDiscountCommandRequest : IRequest<ApplyDiscountCommandResponse>
    {
        public int DiscountId { get; set; }
        public List<int> CarIds { get; set; }
        public int UserId { get; set; }
    }
}
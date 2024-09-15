namespace RentCarApi.Application.Features.Commands.Discount.Create
{
    public class CreateDiscountCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int DiscountId { get; set; }
    }
}
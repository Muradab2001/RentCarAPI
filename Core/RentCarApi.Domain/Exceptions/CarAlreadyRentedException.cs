namespace RentCarApi.Domain.Exceptions;
public class CarAlreadyRentedException : Exception
{
    public CarAlreadyRentedException()
        : base("This car is already rented by another user!")
    {
    }
}

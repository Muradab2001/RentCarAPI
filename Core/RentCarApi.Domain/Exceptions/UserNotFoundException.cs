namespace RentCarApi.Domain.Exceptions
{
    public class UserNotFoundException(string message) : Exception(message)
    {
    }
}
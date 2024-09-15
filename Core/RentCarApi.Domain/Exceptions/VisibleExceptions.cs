using RentCarApi.Domain.Rules;

namespace RentCarApi.Domain.Exceptions
{
    public class VisibleExceptions(string message) : Exception(message), INonSensitiveException
    {
    }
}
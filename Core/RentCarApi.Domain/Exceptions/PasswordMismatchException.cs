using RentCarApi.Domain.Rules;

namespace RentCarApi.Domain.Exceptions
{
    public class PasswordMismatchException(string msg = "Passwords do not match")
        : Exception(msg), INonSensitiveException
    {
    }
}
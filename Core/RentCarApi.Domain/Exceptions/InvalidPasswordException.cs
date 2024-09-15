using RentCarApi.Domain.Rules;

namespace RentCarApi.Domain.Exceptions
{
    public class InvalidPasswordException : Exception, INonSensitiveException
    {
        public InvalidPasswordException() : base("Invalid password!")
        {

        }
    }
}
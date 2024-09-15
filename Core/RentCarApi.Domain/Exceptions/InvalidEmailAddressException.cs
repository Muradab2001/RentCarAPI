using RentCarApi.Domain.Rules;

namespace RentCarApi.Domain.Exceptions
{
    public class InvalidEmailAddressException : Exception, INonSensitiveException
    {
        public InvalidEmailAddressException() : base("Provided email is not associated with any account!")
        {

        }
    }
}
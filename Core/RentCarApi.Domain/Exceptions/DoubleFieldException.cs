using RentCarApi.Domain.Rules;

namespace RentCarApi.Domain.Exceptions
{
    public class DoubleFieldException : Exception, INonSensitiveException
    {
        public DoubleFieldException() : base("Only one field can set")
        {
        }
    }
}
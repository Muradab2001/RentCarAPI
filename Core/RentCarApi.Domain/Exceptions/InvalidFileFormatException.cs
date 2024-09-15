using RentCarApi.Domain.Rules;

namespace RentCarApi.Domain.Exceptions
{
    public class InvalidFileFormatException : Exception, INonSensitiveException, IReversableException
    {
        public InvalidFileFormatException()
           : base("File format is not supported.")
        {

        }
    }
}
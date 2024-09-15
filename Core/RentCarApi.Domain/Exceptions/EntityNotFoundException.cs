namespace RentCarApi.Domain.Exceptions
{
	public class EntityNotFoundException(string message) : Exception(message)
	{
	}
}
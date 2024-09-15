namespace RentCarApi.Domain.Exceptions
{
	public class UserAlreadyExistsException : Exception
	{
		public UserAlreadyExistsException()
		: base("User is already registered. Reset password?")
		{
		}
	}
}

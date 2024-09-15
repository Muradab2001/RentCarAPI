namespace RentCarApi.Domain.Exceptions
{
	public class ConnectionStringNotFoundException : Exception
	{
		public ConnectionStringNotFoundException()
		: base("Connection string not found.")
		{
		}
	}
}
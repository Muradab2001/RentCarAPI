namespace RentCarApi.Application.Common.Interfaces.Email
{
    public interface IEmailManager
    {
        public Task SendEmail(Helpers.Email email);
    }
}
namespace RentCarApi.Application.Common.Interfaces.BackgroundJob
{
    public interface IRentReminderService
    {
        Task SendEndOfRentalEmails();
    }
}
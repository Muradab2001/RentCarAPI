using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Common.Interfaces.BackgroundJob;
using RentCarApi.Application.Common.Interfaces.Email;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;

namespace RentCarApi.Infrastructure.Services;
public class RentReminderService(IUnitOfWork unitOfWork, IEmailManager emailManager) : IRentReminderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailManager _emailManager = emailManager;

    public async Task SendEndOfRentalEmails()
    {
        var tomorrow = DateTime.UtcNow.AddDays(1);
        var orders = await _unitOfWork.GetReadRepository<Order>().Table
            .Include(o => o.Car)
            .Include(o => o.AppUser)
            .Where(o => o.EndTime.Date == tomorrow.Date)
            .ToListAsync();
        foreach (var order in orders)
        {
            var carDescription = order.Car.Description;
            var endTime = order.EndTime;
            var email = new Email()
            {
                To = order.AppUser.Email,
                Subject = "Rental Period Ending Soon",
                Body = $@"
                       <html>
                       <body>
                       <p>
                       <strong>Dear {order.AppUser.UserName},</strong>
                       </p>
                       <br />
                       <p>
                       This is a reminder that your rental for the car '{carDescription}' will end on {endTime}. Please ensure to return the car on time.
                       </p>
                       </body>
                       </html>"
            };
            await _emailManager.SendEmail(email);
            Console.WriteLine("Email gonderildi..");
        }
    }
}
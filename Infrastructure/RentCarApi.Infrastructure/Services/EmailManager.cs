using RentCarApi.Application.Common.Interfaces.Email;
using RentCarApi.Application.Helpers;
using System.Net;
using System.Net.Mail;

namespace RentCarApi.Infrastructure.Services;
public class EmailManager(EmailSettings settings) : IEmailManager
{
    private readonly EmailSettings _settings = settings;

    public async Task SendEmail(Email email)
    {
        using MailMessage message = new();
        message.From = new MailAddress(_settings.From);
        message.To.Add(new MailAddress(email.To));
        message.Subject = email.Subject;
        message.Body = email.Body;
        message.IsBodyHtml = true;

        using var smtpClient = new SmtpClient(_settings.SmtpServer);
        smtpClient.Port = _settings.Port;
        smtpClient.Credentials = new NetworkCredential(_settings.From, _settings.Password);
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(message);
    }
}
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Services;

public class MailSenderService(IConfiguration configuration) : IMailSenderService
{
    public async Task SendAsync(Letter letter)
    {
        using var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress(letter.From, configuration["MailData:UserName"]));
        emailMessage.To.Add(new MailboxAddress("", letter.To));
        emailMessage.Subject = letter.Subject;
        emailMessage.Body = new TextPart(letter.Format)
        {
            Text = letter.Message
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.office365.com", 587, false);
        await client.AuthenticateAsync(configuration["MailData:UserName"], configuration["MailData:Password"]);
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
    }
}
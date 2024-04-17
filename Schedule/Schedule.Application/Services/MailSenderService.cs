using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Schedule.Application.Common.Interfaces;

namespace Schedule.Application.Services;

public class MailSenderService(IConfiguration configuration) : IMailSenderService
{
    public async Task SendAsync(string email, string message)
    {
        using var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress("Schedule automation", configuration["MailData:UserName"]));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Логин и пароль от аккаунта";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = message
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.office365.com", 587, false);
            await client.AuthenticateAsync(configuration["MailData:UserName"], configuration["MailData:Password"]);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }

    public async Task SendHtmlAsync(string email, string html)
    {
        using var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress("Schedule automation", configuration["MailData:UserName"]));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Логин и пароль от аккаунта";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = html
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.office365.com", 587, false);
            await client.AuthenticateAsync(configuration["MailData:UserName"], configuration["MailData:Password"]);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
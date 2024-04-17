namespace Schedule.Application.Common.Interfaces;

public interface IMailSenderService
{
    public Task SendAsync(string email, string message);
    public Task SendHtmlAsync(string email, string html);
}
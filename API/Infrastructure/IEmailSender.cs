namespace API.Infrastructure
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string userEmail, string emailSubject, string msg);
    }
}
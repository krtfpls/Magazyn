using MailKit.Net.Smtp;
using MimeKit;

namespace API.Infrastructure
{
    public class LocalEmailSender: IEmailSender
    {
         private readonly IConfiguration _config;

        public LocalEmailSender(IConfiguration config)
        {
            _config = config;
        }
      
        public async Task SendEmailAsync(string userEmail, string emailSubject, string msg)
        {        
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WMSSrvice Notification" ,_config["From"]));
            message.To.Add(new MailboxAddress("",userEmail));
            message.Subject= emailSubject;
            message.Body= new TextPart(MimeKit.Text.TextFormat.Html) { Text = msg };

            await SendAsync(message);

        }
        private async Task SendAsync(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_config["SmtpServer"], int.Parse(_config["Port"]), true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_config["Username"], _config["Password"]);
                await client.SendAsync(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
    }
   
}

using MailKit.Net.Smtp;
using MimeKit;

namespace API.Infrastructure
{
    public class LocalEmailSender: IEmailSender
    {
         private readonly IConfiguration _config;
         private readonly IHostEnvironment _env;
         private string email_From;
         private string email_SmtpServer;
         private string email_Port;
         private string email_Username;
         private string email_Password;

        public LocalEmailSender(IConfiguration config, IHostEnvironment env)
        {
            _config = config;
            _env = env;
            setMailConfig();
        }

        private void setMailConfig()
        {
            if (_env.IsDevelopment()){
                email_From= _config["EmailConfig:From"];
                email_SmtpServer= _config["EmailConfig:SmtpServer"];
                email_Port= _config["EmailConfig:Port"];
                email_Username= _config["EmailConfig:Username"];
                email_Password=  _config["EmailConfig:Password"];
            }
            else{
                email_From= Environment.GetEnvironmentVariable("EmailConfig_FROM");
                email_SmtpServer= Environment.GetEnvironmentVariable("EmailConfig_SMTPSERVER");
                email_Port= Environment.GetEnvironmentVariable("EmailConfig_PORT");
                email_Username= Environment.GetEnvironmentVariable("EmailConfig_USERNAME");
                email_Password=  Environment.GetEnvironmentVariable("EmailConfig_PASSWORD");
            }
        }

        public async Task SendEmailAsync(string userEmail, string emailSubject, string msg)
        {        
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WMSSrvice Notification", email_From));
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
                        await client.ConnectAsync(email_SmtpServer, int.Parse(email_Port), true);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        await client.AuthenticateAsync(email_Username, email_Password);
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

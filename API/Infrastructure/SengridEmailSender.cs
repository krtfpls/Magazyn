namespace API.Infrastructure
{
    public class SengridEmailSender : IEmailSender
    {
          private readonly IConfiguration _config;
        public SengridEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string userEmail, string emailSubject, string msg)
        {
            // var client = new SendGridClient(_config["SendGrid:Key"]);
            // var message = new SendGridMessage
            // {
            //     From = new EmailAddress("yourEmail@test.com", _config["Sendgrid:User"]),
            //     Subject = emailSubject,
            //     PlainTextContent = msg,
            //     HtmlContent = msg
            // };
            // message.AddTo(new EmailAddress(userEmail));
            // message.SetClickTracking(false, false);

            // await client.SendEmailAsync(message);
        }
}
}
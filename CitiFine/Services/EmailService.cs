using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Citifine Admin", _configuration["Smtp:Username"]));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        emailMessage.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                // Connect to the SendGrid SMTP server using STARTTLS on port 587
                await client.ConnectAsync("smtp.sendgrid.net", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with SendGrid using the API key
                // The username is always "apikey" when using SendGrid SMTP
                var sendGridApiKey = _configuration["Smtp:Password"]; // Store SendGrid API key in your configuration
                await client.AuthenticateAsync("apikey", sendGridApiKey);

                // Send the email
                await client.SendAsync(emailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }

}

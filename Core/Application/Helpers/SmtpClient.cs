using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        // appsettings.json'daki Mail bölümünü oku
        var mailSettings = _configuration.GetSection("Mail");
        var senderEmail = mailSettings["Username"];
        var senderPassword = mailSettings["Password"];
        var host = mailSettings["Host"];

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("KampüsCÜ", senderEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            // Host ve port (587 genelde TLS için kullanılır) ayarlarını kullan
            await client.ConnectAsync(host, 587, SecureSocketOptions.StartTls);

            // Authenticate işlemi için appsettings'den okuduğumuz bilgileri kullanıyoruz
            await client.AuthenticateAsync(senderEmail, senderPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

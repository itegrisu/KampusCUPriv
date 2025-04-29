using Application.Abstractions;

namespace Infrastracture.Services
{
    public class MailService : IMailService
    {
        //private readonly IConfiguration _configuration;
        //private readonly ILogEmailSendWriteRepository _logEmailSendWriteRepository;
        //private readonly IUserReadRepository _userReadRepository;

        //public MailService(IConfiguration configuration, ILogEmailSendWriteRepository logEmailSendWriteRepository, IUserReadRepository userReadRepository)
        //{
        //    _configuration = configuration;
        //    _logEmailSendWriteRepository = logEmailSendWriteRepository;
        //    _userReadRepository = userReadRepository;
        //}

        //public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        //{
        //    await SendMailAsync(new string[] { to }, subject, body, isBodyHtml);

        //}

        //public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        //{
        //    // email loglama yapýlcak kankaaaaa


        //    MailMessage mail = new MailMessage();
        //    mail.IsBodyHtml = isBodyHtml;

        //    foreach (var item in tos)
        //    {
        //        mail.To.Add(item);

        //        User user = await _userReadRepository.GetAsync(predicate: x => x.Email == item);
        //        if (user != null)
        //        {
        //            await _logEmailSendWriteRepository.AddAsync(new LogEmailSend
        //            {
        //                Content = body,
        //                GidUserFK = user.Gid,
        //                Title = subject,
        //            });
        //            await _logEmailSendWriteRepository.SaveAsync();
        //        }


        //    };

        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.From = new MailAddress(_configuration["Mail:Username"], "KampusCU", System.Text.Encoding.UTF8);

        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Credentials = new System.Net.NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    smtp.Host = _configuration["Mail:Host"];

        //    await smtp.SendMailAsync(mail);

        //}

        //public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        //{
        //    StringBuilder mail = new StringBuilder();
        //    mail.AppendLine("Merhaba<br>Eðer yeni þifre talebinde bulunduysanýz aþaðýdaki linkten þifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"......./");
        //    mail.AppendLine(userId);
        //    mail.AppendLine("/");
        //    mail.AppendLine(resetToken);
        //    mail.AppendLine("\">Þifre Yenileme Linki</a></strong><br><br><span style=\"font-size=12px;\" >Not: Talep tarafýnýzca yapýlmadýysa bu maili ciddiye almayýnýz. </span><br><br>Ýyi günler dileriz... <br><br>Kampuscu");
        //    await SendMailAsync(to, "Þifre Yenileme Talebi", mail.ToString());


        //}
    }
}

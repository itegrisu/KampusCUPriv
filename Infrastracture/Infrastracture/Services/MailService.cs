using Application.Abstractions;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Domain.Entities.GeneralManagements;
using Domain.Entities.LogManagements;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Text;

namespace Infrastracture.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogEmailSendWriteRepository _logEmailSendWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public MailService(IConfiguration configuration, ILogEmailSendWriteRepository logEmailSendWriteRepository, IUserReadRepository userReadRepository)
        {
            _configuration = configuration;
            _logEmailSendWriteRepository = logEmailSendWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new string[] { to }, subject, body, isBodyHtml);

        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            // email loglama yap�lcak kankaaaaa


            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;

            foreach (var item in tos)
            {
                mail.To.Add(item);

                User user = await _userReadRepository.GetAsync(predicate: x => x.Email == item);
                if (user != null)
                {
                    await _logEmailSendWriteRepository.AddAsync(new LogEmailSend
                    {
                        Content = body,
                        GidUserFK = user.Gid,
                        Title = subject,
                    });
                    await _logEmailSendWriteRepository.SaveAsync();
                }


            };

            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(_configuration["Mail:Username"], "RementSoft", System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];

            await smtp.SendMailAsync(mail);

        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new StringBuilder();
            mail.AppendLine("Merhaba<br>E�er yeni �ifre talebinde bulunduysan�z a�a��daki linkten �ifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"......./");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">�ifre Yenileme Linki</a></strong><br><br><span style=\"font-size=12px;\" >Not: Talep taraf�n�zca yap�lmad�ysa bu maili ciddiye almay�n�z. </span><br><br>�yi g�nler dileriz... <br><br>Rement Bilgi Teknolojileri");
            await SendMailAsync(to, "�ifre Yenileme Talebi", mail.ToString());


        }
    }
}

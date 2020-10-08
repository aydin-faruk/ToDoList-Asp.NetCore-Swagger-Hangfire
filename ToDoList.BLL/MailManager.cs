using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ToDoList.Entity.DTO.Mail;
using ToDoList.Interface;

namespace ToDoList.BLL
{
    public class MailManager : IMailService
    {
        private readonly SmtpConfigDto _smtpConfigDto;

        public MailManager(IOptions<SmtpConfigDto> options)
        {
            _smtpConfigDto = options.Value;
        }

        public async Task SendMail(MailMessageDto mailMessageDto)
        {
            using var client = CreateSmtpClient();

            mailMessageDto.From = _smtpConfigDto.User;

            MailMessage mailMessage = mailMessageDto.GetMailMessage();
            mailMessage.IsBodyHtml = true;
            await client.SendMailAsync(mailMessage);

        }

        private SmtpClient CreateSmtpClient()
        {
            SmtpClient smtp = new SmtpClient(_smtpConfigDto.Host, _smtpConfigDto.Port);
            smtp.Credentials = new NetworkCredential(_smtpConfigDto.User, _smtpConfigDto.Password);
            smtp.EnableSsl = _smtpConfigDto.UseSsl;
            return smtp;
        }

        public async Task SendMailAsync(MailMessageDto mailMessageDto)
        {
            MailMessage mailMessage = mailMessageDto.GetMailMessage();
            mailMessage.From = new MailAddress(_smtpConfigDto.User);

            using var client = CreateSmtpClient();
            await client.SendMailAsync(mailMessage);
        }
    }
}

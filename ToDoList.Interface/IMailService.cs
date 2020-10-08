using System.Threading.Tasks;
using ToDoList.Entity.DTO.Mail;

namespace ToDoList.Interface
{
    public interface IMailService
    {
        Task SendMail(MailMessageDto mailMessageDto);
    }
}

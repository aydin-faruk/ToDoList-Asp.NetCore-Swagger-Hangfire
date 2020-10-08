using System;
using System.Threading.Tasks;
using ToDoList.Entity.DTO.Mail;
using ToDoList.Entity.Models;
using ToDoList.Interface;

namespace ToDoList.Jobs.Managers.DelayedJobs
{
    public class TaskReminderManager
    {
        private readonly IMailService _mailService;

        public TaskReminderManager(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public async Task Process(Tasks tasks)
        {
            MailMessageDto _mailMessageDto = new MailMessageDto
            {
                Body = "Upcoming Task : " + tasks.Name,

                To = "example@gmail.com",

                Subject = "All Tasks Details",
            };

            await _mailService.SendMail(_mailMessageDto);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.BLL;
using ToDoList.DAL.Concreate.Repository;
using ToDoList.Entity.DTO.Mail;
using ToDoList.Interface;

namespace ToDoList.Jobs.Managers.RecurringJobs
{
    public class SendAllTasksDailyManager
    {
        private readonly ITaskService _taskService = new TaskManager(new EFTaskRepository());
        private readonly IMailService _mailService;

        public SendAllTasksDailyManager(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public async Task Process()
        {
            var planedAndDoneTasks = _taskService.GetAll(x => x.Status == true && x.DueDate > DateTime.Now);
            var planedAndNoDoneTasks = _taskService.GetAll(x => x.Status == true && x.DueDate < DateTime.Now);
            var planedAndCancelTasks = _taskService.GetAll(x => x.Status == false);

            MailMessageDto _mailMessageDto = new MailMessageDto
            {
                Body = "Scheduled and Completed Tasks : " + planedAndDoneTasks +
                " \n Scheduled and Incomplete Tasks : " + planedAndNoDoneTasks +
                " \n Scheduled and Canceled Tasks : " + planedAndCancelTasks,
                
                To = "example@gmail.com",

                Subject = "All Tasks Details",
            };

            await _mailService.SendMail(_mailMessageDto);
        }
    }
}

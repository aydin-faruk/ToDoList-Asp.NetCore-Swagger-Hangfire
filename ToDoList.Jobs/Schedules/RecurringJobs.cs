using Hangfire;
using System;
using ToDoList.Jobs.Managers.RecurringJobs;

namespace ToDoList.Jobs.Schedules
{
    public static class RecurringJobs
    {
        public static void SendAllTasksDaily()//MailMessageDto mailMessageDto
        {
            RecurringJob.RemoveIfExists(nameof(SendAllTasksDailyManager));
            RecurringJob.AddOrUpdate<SendAllTasksDailyManager>(nameof(SendAllTasksDailyManager),
               job => job.Process(), Cron.Daily(12, 30), TimeZoneInfo.Local);
        }
    }
}

using System;
using ToDoList.Entity.Models;
using ToDoList.Jobs.Managers.DelayedJobs;

namespace ToDoList.Jobs.Schedules
{
    public static class DelayedJobs
    {
        public static void ReminderTask(Tasks tasks)
        {
            var reminderDate = tasks.DueDate.AddDays(-1);

            Hangfire.BackgroundJob.Schedule<TaskReminderManager>
                (
                    job => job.Process(tasks),
                    tasks.DueDate
                );
        }
    }
}

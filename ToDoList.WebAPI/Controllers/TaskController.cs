using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BLL;
using ToDoList.DAL.Concreate.Repository;
using ToDoList.Entity.Models;
using ToDoList.Interface;
using ToDoList.Jobs.Schedules;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService = new TaskManager(new EFTaskRepository());

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAll(x => 1 == 1);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.Get(id);

            if (task != null)
                return Ok(task);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Tasks entity)
        {
            var task = await _taskService.Create(entity);
            DelayedJobs.ReminderTask(task);
            return CreatedAtAction("Get", new { id = task.Id}, task);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Tasks entity)
        {
            if (await _taskService.Get(entity.Id) != null)
                return Ok(_taskService.Update(entity));

            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _taskService.Get(id) != null)
            {
                await _taskService.Delete(id);
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Cancel(int id, bool status)
        {
            if (await _taskService.Get(id) != null)
            {
                await _taskService.Cancel(id, status);
                return Ok();
            }
            return NotFound();
        }
    }
}

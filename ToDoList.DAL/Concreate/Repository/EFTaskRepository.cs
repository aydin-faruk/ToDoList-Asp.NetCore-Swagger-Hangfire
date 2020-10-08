using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDoList.DAL.Abstract;
using ToDoList.DAL.Concreate.Entityframework.Context;
using ToDoList.Entity.Models;

namespace ToDoList.DAL.Concreate.Repository
{
    public class EFTaskRepository : ITaskDal
    {
        private readonly ToDoListDbContext _toDoListDbContext = new ToDoListDbContext();

        public async Task<bool> Cancel(int id, bool status)
        {
            var task = await Get(id);
            task.Status = status;

            _toDoListDbContext.Tasks.Update(task);
            await _toDoListDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Tasks> Create(Tasks entity)
        {
            _toDoListDbContext.Tasks.Add(entity);
            await _toDoListDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            _toDoListDbContext.Tasks.Remove(await Get(id));
            await _toDoListDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Tasks> Get(int id)
        {
            return await _toDoListDbContext.Tasks.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Tasks>> GetAll(Expression<Func<Tasks, bool>> predicate)
        {
            return await _toDoListDbContext.Tasks.AsNoTracking().Where(predicate).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Tasks> Update(Tasks entity)
        {
            _toDoListDbContext.Tasks.Update(entity);
            await _toDoListDbContext.SaveChangesAsync();

            return entity;
        }
    }
}

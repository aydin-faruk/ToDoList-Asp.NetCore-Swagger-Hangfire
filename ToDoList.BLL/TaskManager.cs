using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDoList.DAL.Abstract;
using ToDoList.Entity.Models;
using ToDoList.Interface;

namespace ToDoList.BLL
{
    public class TaskManager : ITaskService
    {
        public readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal entity)
        {
            _taskDal = entity;
        }

        public async Task<bool> Cancel(int id, bool status)
        {
            return await _taskDal.Cancel(id, status);
        }

        public async Task<Tasks> Create(Tasks entity)
        {
            return await _taskDal.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _taskDal.Delete(id);
        }

        public async Task<Tasks> Get(int id)
        {
            return await _taskDal.Get(id);
        }

        public async Task<List<Tasks>> GetAll(Expression<Func<Tasks, bool>> predicate)
        {
            return await _taskDal.GetAll(predicate);
        }

        public async Task<Tasks> Update(Tasks entity)
        {
            return await _taskDal.Update(entity);
        }
    }
}

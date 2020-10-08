using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDoList.Entity.Models;

namespace ToDoList.DAL.Abstract
{
    public interface ITaskDal
    {
        Task<List<Tasks>> GetAll(Expression<Func<Tasks, bool>> predicate);
        Task<Tasks> Get(int id);
        Task<Tasks> Create(Tasks entity);
        Task<Tasks> Update(Tasks entity);
        Task<bool> Delete(int id);
        Task<bool> Cancel(int id, bool status);
    }
}

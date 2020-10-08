using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDoList.Interface
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
    }
}

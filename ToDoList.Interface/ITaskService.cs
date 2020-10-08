using System.Threading.Tasks;
using ToDoList.Entity.Models;

namespace ToDoList.Interface
{
    public interface ITaskService : IGenericService<Tasks>
    {
        Task<bool> Cancel(int id, bool status);
    }
}

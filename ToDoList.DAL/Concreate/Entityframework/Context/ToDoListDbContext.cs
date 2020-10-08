using Microsoft.EntityFrameworkCore;
using ToDoList.Entity.Models;

namespace ToDoList.DAL.Concreate.Entityframework.Context
{
    public partial class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext()
        {
        }

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=ToDoListDb;integrated security=true;");
        }

        public DbSet<Tasks> Tasks { get; set; }
    }
}

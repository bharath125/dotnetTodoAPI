using DemoTodo.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DemoTodo.DataBase
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }
        
            public DbSet<TodoTasks> Todo {  get; set; }
        
    }
}

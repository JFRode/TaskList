using Microsoft.EntityFrameworkCore;
using TaskList.Domain;

namespace TaskList.Data.Contexts
{
    public class TasklistDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public TasklistDbContext(DbContextOptions<TasklistDbContext> options) : base(options)
        {
        }
    }
}
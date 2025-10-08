using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace ApiTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<TaskItem> Tasks => Set<TaskItem>(); // Tabla de tareas
    }
}

using ApiTask.Data;

using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace ApiTask.Services
{
    // Servicio que contiene la lógica de negocio de las tareas
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Obtener todas las tareas
        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        // ➕ Crear una nueva tarea
        public async Task<TaskItem> CreateAsync(string title)
        {
            var task = new TaskItem
            {
                Title = title,
                IsCompleted = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        // ✔️ Marcar una tarea como completada
        public async Task<TaskItem?> CompleteAsync(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return task;
        }

        // 🗑️ Eliminar una tarea
        public async Task<bool> DeleteAsync(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

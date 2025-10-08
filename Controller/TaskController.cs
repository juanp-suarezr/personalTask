
using ApiTask.Services;
using Microsoft.AspNetCore.Mvc;
using TasksApi.Models;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // ✅ GET /api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        // ➕ POST /api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem newTask)
        {
            if (string.IsNullOrWhiteSpace(newTask.Title))
                return BadRequest("El título es obligatorio.");

            var createdTask = await _taskService.CreateAsync(newTask.Title);
            return CreatedAtAction(nameof(GetAll), new { id = createdTask.Id }, createdTask);
        }

        // ✔️ PUT /api/tasks/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<ActionResult<TaskItem>> Complete(int id)
        {
            var task = await _taskService.CompleteAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // 🗑️ DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

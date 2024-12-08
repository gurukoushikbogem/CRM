using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        [JwtValidation]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _taskService.GetTasksByUser(userId);
            return tasks.Any() ? Ok(tasks) : NotFound(new { Message = "No tasks found for the specified user." });
        }

        [HttpGet("customer/{customerId}")]
        [JwtValidation]
        public async Task<IActionResult> GetTasksByCustomer(int customerId)
        {
            var tasks = await _taskService.GetTasksByCustomer(customerId);
            return tasks.Any() ? Ok(tasks) : NotFound(new { Message = "No tasks found for the specified customer." });
        }

        [HttpGet("status/{status}")]
        [JwtValidation]
        public async Task<IActionResult> GetTasksByStatus(string status)
        {
            var tasks = await _taskService.GetTasksByStatus(status);
            return tasks.Any() ? Ok(tasks) : NotFound(new { Message = $"No tasks found with status '{status}'." });
        }

        [HttpGet("overdue")]
        [JwtValidation]
        public async Task<IActionResult> GetOverdueTasks()
        {
            var tasks = await _taskService.GetOverdueTasks();
            return tasks.Any() ? Ok(tasks) : Ok(new { Message = "No overdue tasks found." });
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return task != null ? Ok(task) : NotFound(new { Message = "Task not found." });
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddTask( Tasks task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid task data." });

            await _taskService.AddTask(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, new { Message = "Task added successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateTask(Tasks task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid task data." });

            var updated = await _taskService.UpdateTask(task);
            if (updated)
                return Ok(new { Message = "Task updated successfully." });
            return NotFound(new { Message = "Task not found or update failed." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTask(id);
            if (deleted)
                return Ok(new { Message = "Task deleted successfully." });
            return NotFound(new { Message = "Task not found." });
        }

    }
}

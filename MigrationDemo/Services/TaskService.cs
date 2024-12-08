using Microsoft.EntityFrameworkCore;
using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<Tasks>> GetTasksByUser(int userId)
        {
            var tasks = await _taskRepository.GetTasksByUser(userId);
            if (tasks == null || tasks.Count == 0)
                throw new KeyNotFoundException("No tasks found for the specified user.");

            return tasks;
        }

        public async Task<List<Tasks>> GetTasksByCustomer(int customerId)
        {
            var tasks = await _taskRepository.GetTasksByCustomer(customerId);
            if (tasks == null || tasks.Count == 0)
                throw new KeyNotFoundException("No tasks found for the specified customer.");
            return tasks;
        }

        public async Task<List<Tasks>> GetTasksByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be null or empty.");

            var tasks = await _taskRepository.GetTasksByStatus(status);
            if (tasks == null || tasks.Count == 0)
                throw new KeyNotFoundException($"No tasks found with status '{status}'.");
            return tasks;
        }

        public async Task<List<Tasks>> GetOverdueTasks()
        {
            var tasks = await _taskRepository.GetOverdueTasks(DateTime.UtcNow);
            return tasks ?? new List<Tasks>();
        }

        public async Task<Tasks> GetTaskById(int taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found.");
            return task;
        }

        public async Task AddTask(Tasks task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");

            if (string.IsNullOrWhiteSpace(task.TaskDescription))
                throw new ArgumentException("Task description cannot be empty.");

            if (task.DueDate <= DateTime.UtcNow)
                throw new ArgumentException("Due date must be in the future.");

            await _taskRepository.AddTask(task);
        }

        public async Task<bool> UpdateTask(Tasks task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");

            if (string.IsNullOrWhiteSpace(task.TaskDescription))
                throw new ArgumentException("Task description cannot be empty.");

            if (task.DueDate <= DateTime.UtcNow)
                throw new ArgumentException("Due date must be in the future.");

            var updated = await _taskRepository.UpdateTask(task);
            if (!updated)
                throw new KeyNotFoundException("Task not found or update failed.");
            return updated;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var deleted = await _taskRepository.DeleteTask(taskId);
            if (!deleted)
                throw new KeyNotFoundException("Task not found.");
            return deleted;
        }

        public async Task<List<Tasks>> GetUnassignedTask()
        {
            var tasks= await _taskRepository.GetUnassignedTasks();
            return tasks ?? new List<Tasks>();
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return await _taskRepository.GetAllTasks();
        }
    }
}

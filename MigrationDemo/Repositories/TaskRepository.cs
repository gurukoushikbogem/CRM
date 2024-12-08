using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TaskRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tasks>> GetTasksByUser(int userId)
        {
            return await _dbContext.Tasks
                .Where(t => t.AssignedTo == userId)
                .ToListAsync();
        }

        public async Task<List<Tasks>> GetTasksByCustomer(int customerId)
        {
            return await _dbContext.Tasks
                .Where(t => t.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Tasks>> GetTasksByStatus(string status)
        {
            return await _dbContext.Tasks
                .Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<Tasks>> GetOverdueTasks(DateTime currentDate)
        {
            return await _dbContext.Tasks
                .Where(t => t.Status != "Completed" && t.DueDate < currentDate)
                .ToListAsync();
        }

        public async Task AddTask(Tasks task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateTask(Tasks task)
        {
            if (!_dbContext.Tasks.Any(t => t.TaskId == task.TaskId))
                return false;

            _dbContext.Tasks.Update(task);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);
            if (task == null) return false;

            _dbContext.Tasks.Remove(task);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Tasks> GetTaskById(int taskId)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<List<Tasks>> GetUnassignedTasks()
        {
            return await _dbContext.Tasks.Where(t => t.AssignedTo == null).ToListAsync();
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return await _dbContext.Tasks.ToListAsync();
        }
    }
}

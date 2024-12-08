using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetTasksByUser(int userId);
        Task<List<Tasks>> GetTasksByCustomer(int customerId);
        Task<List<Tasks>> GetTasksByStatus(string status);
        Task<List<Tasks>> GetOverdueTasks(DateTime currentDate);
        Task AddTask(Tasks task);
        Task<bool> UpdateTask(Tasks task);
        Task<bool> DeleteTask(int taskId);
        Task<Tasks> GetTaskById(int taskId);
        Task<List<Tasks>> GetUnassignedTasks();
        Task<List<Tasks>> GetAllTasks();
    }

}

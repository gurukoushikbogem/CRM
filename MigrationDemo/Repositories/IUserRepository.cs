using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(int userId);
        Task CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<List<User>> GetUsersByRole(string role);
        Task<List<User>> GetAllUsers();
        Task<List<User>> SearchUsers(string query);
        Task<int?> GetSalespersonIdByName(string name);

        Task<User>GetUserByUserName(string username);

    }
}

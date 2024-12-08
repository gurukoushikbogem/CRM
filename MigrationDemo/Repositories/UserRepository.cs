using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            return await _dbContext.Users.Where(u => u.Role == role).ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<List<User>> SearchUsers(string query)
        {
            return await _dbContext.Users
                .Where(u => u.Username.Contains(query) || u.Email.Contains(query))
                .ToListAsync();
        }

        public async Task<int?> GetSalespersonIdByName(string name)
        {
            var salesperson = await _dbContext.Users
                .Where(u => u.Role == "SalesPerson" && u.Username == name)
                .FirstOrDefaultAsync();

            return salesperson?.UserId;
        }


        public async Task<User> GetUserByUserName(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

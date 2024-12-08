using BCrypt.Net;
using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService jwtsrv;

        public UserService(JwtService jwtsrv, IUserRepository userRepo)
        {
            this.jwtsrv = jwtsrv;
            this._userRepository = userRepo;
        }

        public async Task<(string Token, User UserDetails)> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);
            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateUser(user);

            var token = jwtsrv.GenerateToken(user.Email, user.Role);

            return (token, user);
        }


        public async Task Register(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserByUsername(request.Username);
            if (existingUser != null)
                throw new Exception("Username already exists.");

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = HashPassword(request.Password),
                Role = request.Role,
                Email = request.Email,
                IsActive = true
            };
            await _userRepository.CreateUser(newUser);
        }

        public async Task<bool> UpdateUserStatus(UpdateStatusRequest request)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null) throw new Exception("User not found.");

            user.IsActive = request.IsActive;
            return await _userRepository.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<List<User>> SearchUsers(string query)
        {
            return await _userRepository.SearchUsers(query);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPasswordHash(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<List<User>>GetUserByRole(string role)
        {
            return await _userRepository.GetUsersByRole(role);
        }

        public async Task<int?> GetSalespersonIdByName(string name)
        {
            return await _userRepository.GetSalespersonIdByName(name);
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            return await _userRepository.GetUsersByRole(role);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _userRepository.GetUserByUserName(username);
        }

    }
}

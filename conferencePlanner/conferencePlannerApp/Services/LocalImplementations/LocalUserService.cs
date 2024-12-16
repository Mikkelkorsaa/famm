using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Implementations
{
    public class LocalUserService : IUserService
    {
        private readonly List<User> _users;

        public LocalUserService()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "John Smith",
                    Email = "john.smith@example.com",
                    Role = UserRole.Reviewer,
                    Organization = "Tech University"
                },
                new User
                {
                    Id = 2,
                    Name = "Alice Johnson",
                    Email = "alice.johnson@research.org",
                    Role = UserRole.Admin,
                    Organization = "Research Institute"
                },
                new User
                {
                    Id = 3,
                    Name = "Carlos Rodriguez",
                    Email = "c.rodriguez@science.edu",
                    Role = UserRole.Organizer,
                    Organization = "Science Academy"
                }
            };
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await Task.FromResult(_users.ToList());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to fetch users from the local service.", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                var existingUser = _users.FirstOrDefault(u => u.Id == user.Id)
                    ?? throw new InvalidOperationException($"User with ID {user.Id} not found.");

                var index = _users.IndexOf(existingUser);
                _users[index] = user;

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update user with ID {user.Id}.", ex);
            }
        }
    }
}
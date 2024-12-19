using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;

namespace conferencePlannerApp.Services.Implementations
{
    public class LocalUserService : IUserService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly List<User> _users;
        private const string StorageKey = "currentUser";
        private const string IdStorageKey = "currentUserId";
     
        public LocalUserService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
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

        public async Task<int?> GetCurrentUserIdAsync()
        {
            Console.WriteLine("Test 1");
            var userId = await _localStorage.GetItemAsync<int?>(IdStorageKey);
            Console.WriteLine("Test 2");
            if (userId == 0)
                return 0;
            else
                return userId;
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

        public Task<List<User>> GetUsersBySearchOrFilter(UserFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUsersBySearchOrFilterHits(UserFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
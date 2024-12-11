using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
namespace conferencePlannerApi.Repositories.LocalImplementations
{
    public class LocalUserRepo : IUserRepo
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@doe.com",
                Password = "123",
                Role = UserRole.Admin,
                Organization = "Tech Corp",
                CreatedAt = DateTime.Parse("2024-01-15"),
                IsActive = true
            },
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane@smith.com",
                Password = "123",
                Role = UserRole.Organizer,
                Organization = "Event Masters",
                CreatedAt = DateTime.Parse("2024-01-20"),
                IsActive = true
            },
            new User
            {
                Id = 3,
                Name = "Bob Johnson",
                Email = "bob@johnson.com",
                Password = "123",
                Role = UserRole.Reviewer,
                Organization = "Review Panel Inc",
                CreatedAt = DateTime.Parse("2024-02-01"),
                IsActive = true
            },
            new User
            {
                Id = 4,
                Name = "Sarah Williams",
                Email = "sarah@williams.com",
                Password = "123",
                Role = UserRole.Applicant,
                Organization = "Startup Solutions",
                CreatedAt = DateTime.Parse("2024-02-15"),
                IsActive = true
            }
        };
        private int _lastId = 4;

        public async Task<User?> GetByIdAsync(int id)
            => await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

        public async Task<User?> GetByEmailAsync(string email)
            => await Task.FromResult(_users.FirstOrDefault(u => u.Email == email));

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task<User?> CreateAsync(User user)
        {
            var newUser = user with { Id = ++_lastId, CreatedAt = DateTime.UtcNow };
            _users.Add(newUser);
            return await Task.FromResult(newUser);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index == -1) return null;

            var updatedUser = user with { Id = _users[index].Id };
            _users[index] = updatedUser;
            return await Task.FromResult<User?>(updatedUser);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var index = _users.FindIndex(u => u.Id == id);
            if (index == -1) return false;

            _users.RemoveAt(index);
            return await Task.FromResult(true);
        }
    }
}
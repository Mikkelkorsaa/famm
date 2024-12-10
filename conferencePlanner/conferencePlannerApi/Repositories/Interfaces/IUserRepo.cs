using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
    // Repository interface for User entity operations
    public interface IUserRepo
    {
        // Input: User ID
        // Output: User object or null if not found
        Task<User> GetByIdAsync(int id);

        // Output: Collection of all User objects
        Task<IEnumerable<User>> GetAllAsync();

        // Input: User object without ID
        // Manipulation: Saves to database
        // Output: User object with generated ID
        Task<User> CreateAsync(User user);

        // Input: User ID and updated User object
        // Manipulation: Updates existing record
        // Output: Updated User object or null if not found
        Task<User> UpdateAsync(User user);

        // Input: User ID
        // Manipulation: Removes from database
        // Output: True if deleted, false if not found
        Task<bool> DeleteAsync(int id);

        // Input: Email string
        // Output: User object or null if not found
        Task<User> GetByEmailAsync(string email);
    }
}
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
  public interface IUserRepo
  {
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(int id, User user);
    Task<bool> DeleteAsync(int id);
  }
}
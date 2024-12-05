using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
        public interface IAbstractRepo
        {
                // Input: Abstract ID
                // Output: Abstract object or null if not found
                Task<Abstract?> GetByIdAsync(int id);

                // Output: Collection of all Abstract objects
                Task<IEnumerable<Abstract>> GetAllAsync();

                // Input: Abstract object without ID
                // Manipulation: Saves to database
                // Output: Abstract object with generated ID
                Task<Abstract> CreateAsync(Abstract @abstract);

                // Input: Abstract ID and updated Abstract object
                // Manipulation: Updates existing record
                // Output: Updated Abstract object or null if not found
                Task<Abstract?> UpdateAsync(Abstract @abstract);

                // Input: Abstract ID
                // Manipulation: Removes from database
                // Output: True if deleted, false if not found
                Task<bool> DeleteAsync(int id);

                // Input: Abstract ID and PictureModel object
                // Manipulation: Saves picture to database
                Task<bool> SavePictureAsync(int id, PictureModel picture);
                Task<PictureModel> GetPictureAsync(int id);
        }
}

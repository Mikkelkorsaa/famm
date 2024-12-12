using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
        public interface IAbstractRepo
        {
                // Input: Abstract ID
                // Output: Abstract object or null if not found
                Task<Abstract> GetByIdAsync(int id);

                // Output: Collection of all Abstract objects
                Task<List<Abstract>> GetAllAsync();

                // Input: Abstract object without ID
                // Manipulation: Saves to database
                // Output: Abstract object with generated ID
                Task<Abstract> CreateAsync(Abstract @abstract);

                // Input: Abstract ID and updated Abstract object
                // Manipulation: Updates existing record
                // Output: Updated Abstract object or null if not found
                Task<Abstract> UpdateAsync(Abstract @abstract);

                // Input: Abstract ID
                // Manipulation: Removes from database
                // Output: True if deleted, false if not found
                Task DeleteAsync(int id);


                //Input: Abstract Id & Review
                //Manipulation: Updates the abstract with the review
                //Output: Updated abstract
                Task<Abstract> UpdateReview(int abstractId, Review review);
    }
}

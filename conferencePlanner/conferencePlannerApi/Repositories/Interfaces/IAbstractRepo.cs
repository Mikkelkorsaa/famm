using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
        public interface IAbstractRepo
        {
        /// <summary>
        /// Retrieves an Abstract by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Abstract.</param>
        /// <returns>The Abstract object if found</returns>
        Task<Abstract> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all Abstract entities from the database.
        /// </summary>
        /// <returns>A list containing all Abstract objects.</returns>
        Task<List<Abstract>> GetAllAsync();

        /// <summary>
        /// Creates a new Abstract entity in the database.
        /// </summary>
        /// <param name="abstract">The Abstract object to create (without ID).</param>
        /// <returns>The created Abstract object with its generated ID.</returns>
        Task<Abstract> CreateAsync(Abstract @abstract);

        /// <summary>
        /// Updates an existing Abstract entity in the database.
        /// </summary>
        /// <param name="abstract">The Abstract object with updated values.</param>
        /// <returns>The updated Abstract object if found</returns>
        Task<Abstract> UpdateAsync(Abstract @abstract);

        /// <summary>
        /// Deletes an Abstract entity from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the Abstract to delete.</param>
        /// <returns>True if the Abstract was deleted <returns>
        Task DeleteAsync(int id);


        /// <summary>
        /// Updates the review for a specific Abstract entity.
        /// </summary>
        /// <param name="abstractId">The unique identifier of the Abstract.</param>
        /// <param name="review">The Review object containing the updated review information.</param>
        /// <returns>The Abstract object with the updated review.</returns>
        Task<Abstract> UpdateReview(int abstractId, Review review);
    }
}

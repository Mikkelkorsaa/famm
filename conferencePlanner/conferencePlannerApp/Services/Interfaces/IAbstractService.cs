using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IAbstractService
    {
        /// <summary>
        /// Adds a new Abstract object to the database
        /// </summary>
        /// <param name="_abstract">The Abstract object to be added</param>
        /// <returns>The added Abstract object</returns>
        Task<Abstract> AddAbstract(Abstract _abstract);

        /// <summary>
        /// Retrieves all Abstract objects from the database
        /// </summary>
        /// <returns>A list of Abstract objects</returns>
        Task<List<Abstract>> GetAbstracts();

        /// <summary>
        /// Updates an existing Abstract object in the database
        /// </summary>
        /// <param name="_abstract">The Abstract object to be updated</param>
        /// <returns>True if the update was successful, otherwise false</returns>
        Task<bool> UpdateAbstract(Abstract _abstract);

        /// <summary>
        /// Deletes an Abstract object from the database
        /// </summary>
        /// <param name="_abstract">The Abstract object to be deleted</param>
        Task DeleteAbstract(Abstract _abstract);

        /// <summary>
        /// Updates the review of an Abstract
        /// </summary>
        /// <param name="abstractId">The ID of the Abstract</param>
        /// <param name="review">The Review object to be updated</param>
        /// <returns>The updated Abstract object</returns>
        Task<Abstract> UpdateReview(int abstractId, Review review);

        /// <summary>
        /// Retrieves all Abstract objects associated with a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <returns>A list of Abstract objects</returns>
        Task<List<Abstract>> GetAllAbstractsByConferenceIdAsync(int conferenceId);

        /// <summary>
        /// Checks if a user has already reviewed a specific Abstract
        /// </summary>
        /// <param name="abstractId">The ID of the Abstract</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>True if the user has reviewed the Abstract, otherwise false</returns>
        Task<bool> HasReviewAsync(int abstractId, int userId);

        /// <summary>
        /// Generates a unique review ID for a specific Abstract
        /// </summary>
        /// <param name="abstractId">The ID of the Abstract</param>
        /// <returns>A unique review ID</returns>
        Task<int> GetNextReviewIdAsync(int abstractId);

        /// <summary>
        /// Retrieves an Abstract object by its ID
        /// </summary>
        /// <param name="abstractId">The ID of the Abstract</param>
        /// <returns>The Abstract object</returns>
        Task<Abstract> GetById(int abstractId);

        /// <summary>
        /// Retrieves an existing Review object for a specific Abstract and user
        /// </summary>
        /// <param name="abstractId">The ID of the Abstract</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>The Review object</returns>
        Task<Review> GetExistingReviewAsync(int abstractId, int userId);
    }
}

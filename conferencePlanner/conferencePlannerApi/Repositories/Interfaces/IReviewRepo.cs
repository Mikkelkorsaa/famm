using conferencePlannerCore.Models;
namespace conferencePlannerApi.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        /// <summary>
        /// Retrieves a Review by its id.
        /// </summary>
        /// <param name="id">The id of the review.</param>
        /// <returns>The review object associated with the id</returns>
        Task<Review> GetReviewByIdAsync(int id);

        /// <summary>
        /// Updates an Abstract with a new Review.
        /// </summary>
        /// <param name="id">The unique identifier of the abstract.</param>
        /// <param name="review">The Review object containing the updated review information.</param>
        /// <returns>The updated Review object</returns>
        Task<Review> UpdateAbstract(int id, Review review);


    }
}

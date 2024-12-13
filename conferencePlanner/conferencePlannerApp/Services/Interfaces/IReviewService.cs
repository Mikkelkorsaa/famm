using conferencePlannerCore.Models;
namespace conferencePlannerApp.Services.Interfaces

{
    public interface IReviewService
    {
        /// <summary>
        /// Gets the review of an abstract
        /// </summary>
        /// <param name="abstractId"></param>
        /// <returns>Returns a review object</returns>
        Task<Review> GetReviewByAbstractIdAsync(int abstractId); 
    }
}

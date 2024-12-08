using conferencePlannerCore.Models;
namespace conferencePlannerApi.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        // Input: The Id of the review
        // Manipulation: 
        // Output: returns the review with the given Id
        Task<Review> GetReviewByIdAsync(int id);

        // Input: The Id of the abstract and the review
        // Manipulation: saves the review to the database
        // Output: 
        Task<Review> UpdateAbstract(int id, Review review);


    }
}

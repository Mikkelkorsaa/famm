
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
	public interface IAbstractService
	{
		//Input: Abstract obj.
		//Manipulation: Sends abstract to database
		Task<Abstract> AddAbstract(Abstract _abstract);

		//Output: returns a List of abstract objects
		Task<List<Abstract>> GetAbstracts();

		//Input: Abstract obj.
		//Manipulation: 1. Finds abstract in Db with matching id 2.Updates the existing obj. in the Db with the current obj. from clientside
		Task<bool> UpdateAbstract (Abstract _abstract);

		//Input: Abstract obj.
		//Manipulation: Find abstract with matching id in DB and then deletes it
		
		Task DeleteAbstract (Abstract _abstract);

		Task<Abstract> UpdateReview(int abstractId, Review review);
		/// <summary>
		/// Find all the abstracts under a given conference
		/// </summary>
		/// <param name="conferenceId"></param>
		/// <returns>A list of abstracts</returns>
		Task<List<Abstract>> GetAllAbstractsByConferenceIdAsync(int conferenceId);
		/// <summary>
		/// Checks if a user has reviewed already
		/// </summary>
		/// <param name="abstractId"></param>
		/// <param name="userId"></param>
		/// <returns>true or false</returns>
		Task<bool> HasReviewAsync(int abstractId, int userId);
		/// <summary>
		/// ensures that you get a unique id for your review in an abstract
		/// </summary>
		/// <param name="abstractId"></param>
		/// <returns>a unique reviewId </returns>
		Task<int> GetNextReviewIdAsync(int abstractId);
		/// <summary>
		/// Get the abstract using the id
		/// </summary>
		/// <param name="abstractId"></param>
		/// <returns>Abstract Object</returns>
		Task<Abstract> GetById(int abstractId);
        /// <summary>
        /// Get the review using the abstractId and userId
        /// </summary>
        /// <param name="abstractId"></param>
        /// <param name="userId"></param>
        /// <returns>Review object</returns>
        Task<Review> GetExistingReviewAsync(int abstractId, int userId);




    }
}

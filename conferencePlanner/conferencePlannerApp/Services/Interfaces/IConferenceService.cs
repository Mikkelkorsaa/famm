﻿using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IConferenceService
    {
        // A user want to create a conference, the site needs to provide a conference,
        // The method needs to store the conference.
        Task<Conference> CreateConferenceAsync(Conference conference);
        //A user want to see all conferences that have not yet concluded.
        //The method will return all conferences with a EndDate in the future.
        Task<IEnumerable<Conference>> GetActiveConferencesAsync();
		/// <summary>
		/// Returns all conferences
		/// </summary>
		/// <returns> List of conference objects</returns>
		Task<List<Conference>> GetConferencesAsync();
		/// <summary>
		/// Gets the conference by the ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Conference object</returns>
		///
		Task<Conference> GetByIdAsync(int id);
        Task<List<Conference>> GetActiveConferencesAsync();
        /// <summary>
        /// Gets the conference by the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Conference object</returns>
        Task<Conference> GetByIdAsync(int id);
        /// <summary>
        /// Updates the conference with matching id in the DB and
        /// then it returns the same object back
        /// </summary>
        /// <param name="conference"></param>
        /// <returns>The updated Conference Objekt</returns>
        Task<Conference> UpdateAsync(Conference conference);
        /// <summary>
        /// gets the conference from the cashe. if no cashe contact api
        /// </summary>
        /// <returns>Conference Id</returns>
        Task<int?> GetCurrentConferenceIdAsync();
        /// <summary>
        /// sets the conference id in localstorage
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Conference Object</returns>
        Task<Conference> SetCurrentConferenceAsync(int id);
        /// <summary>
        /// Gets all abstracts in a conference
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of abstracs</returns>
        Task<List<Abstract>> GetAllAbstractsByIdAsync(int id);
        
        /// <summary>
        /// Updates the conference with a new review
        /// </summary>
        /// <param name="abstractId"></param>
        /// <param name="review"></param>
        /// <returns></returns>
        Task UpdateReview(int abstractId, Review review);
        /// <summary>
        /// Get the review criterias for a specific conference using the id
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns>returns a list of the review criteria</returns>
        Task<List<string>> GetCriteriaByIdAsync(int conferenceId);
        /// <summary>
        /// Input: int Conference.ID
        /// Output: The list of categories bound to this conference.
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns></returns>
        Task<List<string>> GetCategoriesByIdAsync(int conferenceId);
    }
}

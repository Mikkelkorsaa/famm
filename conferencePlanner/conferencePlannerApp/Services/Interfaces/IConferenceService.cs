﻿using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IConferenceService
    {
        // A user want to create a conference, the site needs to provide a conference,
        // The method needs to store the conference.
        Task CreateConferenceAsync(Conference conference);
        //A user want to see all conferences that have not yet concluded.
        //The method will return all conferences with a EndDate in the future.
        Task<IEnumerable<Conference>> GetActiveConferencesAsync();
        /// <summary>
        /// Gets the conference by the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Conference object</returns>
        Task<Conference> GetByIdAsync(int id);
        /// <summary>
        /// gets the conference from the cashe. if no cashe contact api
        /// </summary>
        /// <returns>Conference object</returns>
        Task<Conference> GetCurrentConferenceIdAsync();
        /// <summary>
        /// sets the conference id in localstorage
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Conference Object</returns>
        Task<Conference> SetCurrentConferenceAsync(int id);
    }
}

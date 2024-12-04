using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IConferenceHandler
    {
        // A user want to create a conference, the site needs to provide a conference,
        // The method needs to store the conference.
        public Task createConference(Conference conference);
        //A user wants to access information about a conference
        //The method uses the ID to retrieve the object
        public Task<Conference> getConference(int id);

        //A user want to see all conferences that have not yet concluded.
        //The method will return all conferences with an EndDate in the future.
        public Task<List<Conference>> getActiveConferences(ConferenceFilter filter);
        //A user wants to see past conferences.
        //The method will return all conferences with an EndDate int the past. 
        public Task<List<Conference>> getPastConferences(ConferenceFilter filter);
    }
}

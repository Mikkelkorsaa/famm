using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IConferenceHandler
    {
        // A user want to create a conference, the site needs to provide a conference,
        // The method needs to store the conference.
        public Task createConference(Conference conference);
        //A user want to see all conferences that have not yet concluded.
        //The method will return all conferences with a EndDate in the future.
        public Task<List<Conference>> getActiveConferences(ConferenceFilter filter);
    }
}

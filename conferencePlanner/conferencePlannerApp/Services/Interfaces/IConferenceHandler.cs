using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IConferenceHandler
    {
        // A user want to create a conference, the site needs to provide a conference,
        // The method needs to store the conference.
        public Task createConference(Conference conference); 
    }
}

using System.ComponentModel.DataAnnotations;

namespace conferencePlannerCore.Models
{
    public record Abstract
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string PresenterEmail { get; set; } = string.Empty;
        public List<string> CoAuthors { get; set; } = new List<string>();
        public string Organization { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string KeyValues { get; set; } = string.Empty;
        public string AbstractText { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
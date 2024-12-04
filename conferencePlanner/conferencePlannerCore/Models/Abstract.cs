using System.ComponentModel.DataAnnotations;

namespace conferencePlannerCore.Models
{
    public record Abstract
    {
        public int Id { get; init; }
        public int ConferenceId { get; init; }
        public string SenderName { get; set; } = string.Empty;
        public string PresenterEmail { get; set; } = string.Empty;
        public List<string> CoAuthors { get; set; } = new List<string>();
        public string Organization { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        [StringLength(400)]
        public string KeyValues { get; set; } = string.Empty;
        [StringLength(2000)]
        public string AbstractText { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public byte[] Picture { get; set; } = Array.Empty<byte>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
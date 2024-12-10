namespace conferencePlannerCore.Models
{
    public record ConferenceFilter
    {
        public DateTime? EndDate { get; set; } = DateTime.Now;
    }
}

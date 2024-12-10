namespace conferencePlannerCore.Models
{
    public record Conference
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime AbstractDeadLine { get; set; }
        public DateTime ReviewDeadline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Category { get; set; } = new();
        public List<string> ReviewCriteria { get; set; } = new();
        public Venue Location { get; set; } = new();
    }
}

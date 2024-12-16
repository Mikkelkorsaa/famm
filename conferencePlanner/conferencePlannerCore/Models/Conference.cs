namespace conferencePlannerCore.Models
{
    public record Conference
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = string.Empty;
        public DateTime AbstractDeadLine { get; set; }
        public DateTime ReviewDeadline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Category { get; set; } = new();
        public List<string> ReviewCriteria { get; set; } = new();
        public Venue Location { get; set; } = new();
        public List<Abstract> Abstracts { get; set; } = new();
        public ConferencePlan Plan { get; set; } = new();
        public List<Review> Reviewers { get; set; } = new();
    }
}
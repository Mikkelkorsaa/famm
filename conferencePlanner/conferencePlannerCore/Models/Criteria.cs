namespace conferencePlannerCore.Models
{
    public record Criteria
    {
        public string Name { get; set; } = string.Empty;
        public int? Grade { get; set; }
    }
}

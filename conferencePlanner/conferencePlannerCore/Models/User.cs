namespace conferencePlannerCore.Models
{
    public record User
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public bool IsActive { get; init; } = true;
    }
}
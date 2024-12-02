namespace conferencePlannerCore.Models
{
    public record LoginModel
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
namespace conferencePlannerCore.Models
{
    public record User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Applicant;
        public string Organization { get; set; } = string.Empty;
        // public string? ProfilePictureUrl { get; set; }
        // public string? OrganizationLogoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public enum UserRole
    {
        Admin,
        Organizer,
        Applicant,
        Reviewer
    }
}
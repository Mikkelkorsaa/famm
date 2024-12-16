namespace conferencePlannerCore.Models
{
  public record RegisterModel
  {
    /// <summary>
    /// The name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// The email of the user
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// The password of the user
    /// </summary>
    public string Password { get; set; } = string.Empty;
    /// <summary>
    /// The password confirmation of the user
    /// </summary>
    public string ConfirmPassword { get; set; } = string.Empty;
    /// <summary>
    /// The organization of the user
    /// </summary>
    public string Organization { get; set; } = string.Empty;
  }
}
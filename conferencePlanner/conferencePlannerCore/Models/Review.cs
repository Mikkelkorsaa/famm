using System.ComponentModel.DataAnnotations;
namespace conferencePlannerCore.Models;

public record Review
{
    public int Id { get; set; }
    public int UserId{ get; set; }

    public List<Criteria> Criterias { get; set; } = new List<Criteria>();

    public string? Comment { get; set; }
    
    public bool Recommend { get; set; }
}
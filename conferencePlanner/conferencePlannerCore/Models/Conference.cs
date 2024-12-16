using System.ComponentModel.DataAnnotations;

namespace conferencePlannerCore.Models
{
    public class Conference
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
        public List<Reviewer> Reviewers { get; set; } = new();
        public ConferencePlan Plan { get; set; } = new();
        
        public void removeCategory(string category) {
            if (category != null) {
                foreach (Reviewer r in Reviewers) {
                    if(r.Categories.Contains(category)) { 
                        r.Categories.Remove(category);
                    }
                }
                Category.Remove(category);
            }
            return;            
        }
        
    }
}
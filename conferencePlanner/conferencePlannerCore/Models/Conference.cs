using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AbstractDeadLine { get; set; }
        public DateTime ReviewDeadline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Category { get; set; }
        public List<string> ReviewCriteria { get; set; }
        public Venue Venue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public class Reviewer
    {
        public User User { get; set; }
        public List<string> Category { get; set; }
    }
}

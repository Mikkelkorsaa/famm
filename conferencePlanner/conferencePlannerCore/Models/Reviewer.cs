using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public class Reviewer
    {
        [Required]
        public int UserId { get; set; } = -1;
        public string NameUser { get; set; } = string.Empty;
        [Required]
        public List<string> Categories { get; set; } = new();
    }
}

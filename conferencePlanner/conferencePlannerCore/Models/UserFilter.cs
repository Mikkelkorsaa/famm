using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public class UserFilter
    {
        [Required]
        public string Query { get; set; } = string.Empty;
        public int numberOfUsersShown {get; set;} = 0;
        public int numberOfUsersSkipped { get; set; } = 0;
    }
}

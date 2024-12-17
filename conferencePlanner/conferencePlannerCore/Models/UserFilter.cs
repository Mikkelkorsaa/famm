using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public class UserFilter
    {
        public string Query { get; set; } = string.Empty;
        public int numberOfUsersShown {get; set;} = 0;
        public int numberOfUsersSkipped { get; set; } = 0;
    }
}

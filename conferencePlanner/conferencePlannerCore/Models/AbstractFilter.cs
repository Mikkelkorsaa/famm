using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public class AbstractFilter
    {
        public List<string> categories { get; set; } = new();
        public string searchWord { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models
{
    public record ConferenceFilter
    {
        public DateTime? EndDate { get; set; } = DateTime.Now;
    }
}

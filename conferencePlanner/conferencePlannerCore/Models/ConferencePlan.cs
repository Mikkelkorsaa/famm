using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models;
    public class ConferencePlan
    {
    public int Id { get; set; }
    public List<SchedulerEvent> Events { get; set; } = new();
}


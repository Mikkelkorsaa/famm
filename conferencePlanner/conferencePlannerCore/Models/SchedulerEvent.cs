using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models;
public class SchedulerEvent
{
	public int Id { get; set; } = 0;
	public string Title { get; set; } = string.Empty;
	public DateTime Start { get; set; } = DateTime.Now;
	public DateTime End { get; set; } = DateTime.Now;
	public int RoomId { get; set; } = 0;
	public string Color { get; set; } = string.Empty;
}


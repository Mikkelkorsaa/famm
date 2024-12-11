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
        public int conferenceId { get; set; } = -1;
        public int senderUserId { get; set; } = -1;
        public int userHaveReviewedId { get; set; } = -1;
        public string searchWord { get; set; } = string.Empty;
    }
}

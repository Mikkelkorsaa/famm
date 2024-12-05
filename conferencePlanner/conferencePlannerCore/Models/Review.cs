using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conferencePlannerCore.Models;

public record Review
{
    public int Id { get; set; }
    public int UserId{ get; set; }

    public List<Criteria> Criterias { get; set; } = new List<Criteria>();

    [StringLength(200)]
    public string Comment { get; set; }
}


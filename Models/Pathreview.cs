using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class Pathreview
{
    public int PathId { get; set; }

    public int? InstructorId { get; set; }

    public string? Review { get; set; }

    public virtual Instructor? Instructor { get; set; }
}

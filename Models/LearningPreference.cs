using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class LearningPreference
{
    public int LearnerId { get; set; }

    public string? Preference { get; set; }

    public virtual Learner Learner { get; set; } = null!;
}

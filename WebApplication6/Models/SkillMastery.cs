﻿using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class SkillMastery
{
    public int QuestId { get; set; }

    public string Skill { get; set; } = null!;

    public virtual ICollection<LearnerMastery> LearnerMasteries { get; set; } = new List<LearnerMastery>();

    public virtual Quest Quest { get; set; } = null!;
}

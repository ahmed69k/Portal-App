﻿using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class Quest
{
    public int QuestId { get; set; }

    public string? DifficultyLevel { get; set; }

    public string? Criteria { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public virtual Collaborative? Collaborative { get; set; }

    public virtual ICollection<QuestReward> QuestRewards { get; set; } = new List<QuestReward>();

    public virtual SkillMastery? SkillMastery { get; set; }
}

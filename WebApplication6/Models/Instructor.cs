using System;
using System.Collections.Generic;

namespace WebApplication6.Models
{
    public partial class Instructor
    {
        public int InstructorId { get; set; } // Primary key for Instructor table
        public string? Name { get; set; }
        public string? LatestQualification { get; set; }
        public string? ExpertiseArea { get; set; }
        public string? Email { get; set; }

        // Removed UserId as it is redundant
        public virtual ICollection<EmotionalfeedbackReview> EmotionalfeedbackReviews { get; set; } = new List<EmotionalfeedbackReview>();
        public virtual ICollection<Pathreview> Pathreviews { get; set; } = new List<Pathreview>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

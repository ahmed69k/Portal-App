using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Models;

public partial class Fm2Context : DbContext
{
    public Fm2Context()
    {
    }

    public Fm2Context(DbContextOptions<Fm2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Assessment> Assessments { get; set; }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<Collaborative> Collaboratives { get; set; }

    public virtual DbSet<ContentLibrary> ContentLibraries { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<DiscussionForum> DiscussionForums { get; set; }

    public virtual DbSet<EmotionalFeedback> EmotionalFeedbacks { get; set; }

    public virtual DbSet<EmotionalfeedbackReview> EmotionalfeedbackReviews { get; set; }

    public virtual DbSet<FilledSurvey> FilledSurveys { get; set; }

    public virtual DbSet<HealthCondition> HealthConditions { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<InteractionLog> InteractionLogs { get; set; }

    public virtual DbSet<Leaderboard> Leaderboards { get; set; }

    public virtual DbSet<Learner> Learners { get; set; }

    public virtual DbSet<LearnerDiscussion> LearnerDiscussions { get; set; }

    public virtual DbSet<LearnerMastery> LearnerMasteries { get; set; }

    public virtual DbSet<LearnersCollaboration> LearnersCollaborations { get; set; }

    public virtual DbSet<LearningActivity> LearningActivities { get; set; }

    public virtual DbSet<LearningGoal> LearningGoals { get; set; }

    public virtual DbSet<LearningPath> LearningPaths { get; set; }

    public virtual DbSet<LearningPreference> LearningPreferences { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleContent> ModuleContents { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Pathreview> Pathreviews { get; set; }

    public virtual DbSet<PersonalizationProfile> PersonalizationProfiles { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<QuestReward> QuestRewards { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<Reward> Rewards { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillMastery> SkillMasteries { get; set; }

    public virtual DbSet<SkillProgression> SkillProgressions { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }

    public virtual DbSet<Takenassessment> Takenassessments { get; set; }

    public virtual DbSet<TargetTrait> TargetTraits { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FM2;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.AchievementId).HasName("PK__Achievem__276330E0270D96D5");

            entity.ToTable("Achievement");

            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
            entity.Property(e => e.BadgeId).HasColumnName("BadgeID");
            entity.Property(e => e.DateEarned).HasColumnName("date_earned");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Badge).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.BadgeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Achieveme__Badge__1BC821DD");

            entity.HasOne(d => d.Learner).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.LearnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Achieveme__Learn__1AD3FDA4");
        });

        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assessme__3214EC271B42907A");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Criteria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("criteria");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.PassingMarks).HasColumnName("passing_marks");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TotalMarks).HasColumnName("total_marks");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Weightage).HasColumnName("weightage");

            entity.HasOne(d => d.Module).WithMany(p => p.Assessments)
                .HasForeignKey(d => new { d.ModuleId, d.CourseId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Assessments__5535A963");
        });

        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.BadgeId).HasName("PK__Badge__1918237CE2ED6FF5");

            entity.ToTable("Badge");

            entity.Property(e => e.BadgeId)
                .ValueGeneratedNever()
                .HasColumnName("BadgeID");
            entity.Property(e => e.Criteria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("criteria");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Collaborative>(entity =>
        {
            entity.HasKey(e => e.QuestId).HasName("PK__Collabor__B6619ACBA711EE96");

            entity.ToTable("Collaborative");

            entity.Property(e => e.QuestId)
                .ValueGeneratedNever()
                .HasColumnName("QuestID");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.MaxNumParticipants).HasColumnName("max_num_participants");

            entity.HasOne(d => d.Quest).WithOne(p => p.Collaborative)
                .HasForeignKey<Collaborative>(d => d.QuestId)
                .HasConstraintName("FK__Collabora__Quest__2645B050");
        });

        modelBuilder.Entity<ContentLibrary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContentL__3214EC27C7CFFC8B");

            entity.ToTable("ContentLibrary");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContentUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content_URL");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Metadata)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metadata");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Module).WithMany(p => p.ContentLibraries)
                .HasForeignKey(d => new { d.ModuleId, d.CourseId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ContentLibrary__52593CB8");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D718769AE5F94");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.CreditPoints).HasColumnName("credit_points");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("difficulty_level");
            entity.Property(e => e.LearningObjective)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("learning_objective");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasMany(d => d.Courses).WithMany(p => p.Prereqs)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursePrerequisite",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CoursePre__Cours__45F365D3"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("Prereq")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CoursePre__Prere__46E78A0C"),
                    j =>
                    {
                        j.HasKey("CourseId", "Prereq").HasName("PK__CoursePr__F8693C2CD4B9D900");
                        j.ToTable("CoursePrerequisite");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                    });

            entity.HasMany(d => d.Prereqs).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursePrerequisite",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("Prereq")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CoursePre__Prere__46E78A0C"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CoursePre__Cours__45F365D3"),
                    j =>
                    {
                        j.HasKey("CourseId", "Prereq").HasName("PK__CoursePr__F8693C2CD4B9D900");
                        j.ToTable("CoursePrerequisite");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                    });
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Course_e__7F6877FB1FE42095");

            entity.ToTable("Course_enrollment");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CompletionDate).HasColumnName("completion_date");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("enrollment_date");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("In Progress")
                .HasColumnName("status");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Course_en__Cours__73BA3083");

            entity.HasOne(d => d.Learner).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.LearnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Course_en__Learn__74AE54BC");
        });

        modelBuilder.Entity<DiscussionForum>(entity =>
        {
            entity.HasKey(e => e.ForumId).HasName("PK__Discussi__E210AC4FEBAF192A");

            entity.ToTable("Discussion_forum");

            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LastActive)
                .HasColumnType("datetime")
                .HasColumnName("last_active");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Module).WithMany(p => p.DiscussionForums)
                .HasForeignKey(d => new { d.ModuleId, d.CourseId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Discussion_forum__30C33EC3");
        });

        modelBuilder.Entity<EmotionalFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Emotiona__6A4BEDF601950633");

            entity.ToTable("Emotional_feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.ActivityId).HasColumnName("activityID");
            entity.Property(e => e.EmotionalState)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("emotional_state");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Activity).WithMany(p => p.EmotionalFeedbacks)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Emotional__activ__6383C8BA");

            entity.HasOne(d => d.Learner).WithMany(p => p.EmotionalFeedbacks)
                .HasForeignKey(d => d.LearnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Emotional__Learn__628FA481");
        });

        modelBuilder.Entity<EmotionalfeedbackReview>(entity =>
        {
            entity.HasKey(e => new { e.FeedbackId, e.InstructorId }).HasName("PK__Emotiona__C39BFD41475B7430");

            entity.ToTable("Emotionalfeedback_review");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            entity.Property(e => e.Review)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("review");

            entity.HasOne(d => d.Feedback).WithMany(p => p.EmotionalfeedbackReviews)
                .HasForeignKey(d => d.FeedbackId)
                .HasConstraintName("FK__Emotional__Feedb__6E01572D");

            entity.HasOne(d => d.Instructor).WithMany(p => p.EmotionalfeedbackReviews)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK__Emotional__Instr__6EF57B66");
        });

        modelBuilder.Entity<FilledSurvey>(entity =>
        {
            entity.HasKey(e => new { e.SurveyId, e.Question, e.LearnerId }).HasName("PK__FilledSu__D89C33C72EB2C16B");

            entity.ToTable("FilledSurvey");

            entity.Property(e => e.SurveyId).HasColumnName("SurveyID");
            entity.Property(e => e.Question)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Answer)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Learner).WithMany(p => p.FilledSurveys)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__FilledSur__Learn__0D7A0286");

            entity.HasOne(d => d.SurveyQuestion).WithMany(p => p.FilledSurveys)
                .HasForeignKey(d => new { d.SurveyId, d.Question })
                .HasConstraintName("FK__FilledSurvey__0C85DE4D");
        });

        modelBuilder.Entity<HealthCondition>(entity =>
        {
            entity.HasKey(e => new { e.LearnerId, e.ProfileId, e.Condition }).HasName("PK__HealthCo__930320B08BD1B855");

            entity.ToTable("HealthCondition");

            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("condition");

            entity.HasOne(d => d.PersonalizationProfile).WithMany(p => p.HealthConditions)
                .HasForeignKey(d => new { d.LearnerId, d.ProfileId })
                .HasConstraintName("FK__HealthCondition__412EB0B6");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__9D010B7BB682F73F");

            entity.ToTable("Instructor");

            entity.Property(e => e.InstructorId)
                .ValueGeneratedNever()
                .HasColumnName("InstructorID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.ExpertiseArea)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("expertise_area");
            entity.Property(e => e.LatestQualification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("latest_qualification");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasMany(d => d.Courses).WithMany(p => p.Instructors)
                .UsingEntity<Dictionary<string, object>>(
                    "Teach",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Teaches__CourseI__787EE5A0"),
                    l => l.HasOne<Instructor>().WithMany()
                        .HasForeignKey("InstructorId")
                        .HasConstraintName("FK__Teaches__Instruc__778AC167"),
                    j =>
                    {
                        j.HasKey("InstructorId", "CourseId").HasName("PK__Teaches__F193DC635A4CB8CE");
                        j.ToTable("Teaches");
                        j.IndexerProperty<int>("InstructorId").HasColumnName("InstructorID");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                    });
        });

        modelBuilder.Entity<InteractionLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Interact__5E5499A82D79211A");

            entity.ToTable("Interaction_log");

            entity.Property(e => e.LogId)
                .ValueGeneratedNever()
                .HasColumnName("LogID");
            entity.Property(e => e.ActionType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("action_type");
            entity.Property(e => e.ActivityId).HasColumnName("activity_ID");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.Activity).WithMany(p => p.InteractionLogs)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Interacti__activ__5EBF139D");

            entity.HasOne(d => d.Learner).WithMany(p => p.InteractionLogs)
                .HasForeignKey(d => d.LearnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Interacti__Learn__5FB337D6");
        });

        modelBuilder.Entity<Leaderboard>(entity =>
        {
            entity.HasKey(e => e.BoardId).HasName("PK__Leaderbo__F9646BD262AEC362");

            entity.ToTable("Leaderboard");

            entity.Property(e => e.BoardId)
                .ValueGeneratedNever()
                .HasColumnName("BoardID");
            entity.Property(e => e.Season)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("season");
        });

        modelBuilder.Entity<Learner>(entity =>
        {
            entity.HasKey(e => e.LearnerId).HasName("PK__Learner__67ABFCFA3C1358A3");

            entity.ToTable("Learner");

            entity.Property(e => e.LearnerId)
                .ValueGeneratedNever()
                .HasColumnName("LearnerID");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CulturalBackground)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cultural_background");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<LearnerDiscussion>(entity =>
        {
            entity.HasKey(e => new { e.ForumId, e.LearnerId }).HasName("PK__LearnerD__546A1380C98B1B35");

            entity.ToTable("LearnerDiscussion");

            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Post)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");

            entity.HasOne(d => d.Forum).WithMany(p => p.LearnerDiscussions)
                .HasForeignKey(d => d.ForumId)
                .HasConstraintName("FK__LearnerDi__Forum__339FAB6E");

            entity.HasOne(d => d.Learner).WithMany(p => p.LearnerDiscussions)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__LearnerDi__Learn__3493CFA7");
        });

        modelBuilder.Entity<LearnerMastery>(entity =>
        {
            entity.HasKey(e => new { e.LearnerId, e.QuestId }).HasName("PK__LearnerM__CCCDE55661334F29");

            entity.ToTable("LearnerMastery");

            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.QuestId).HasColumnName("QuestID");
            entity.Property(e => e.CompletionStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("completion_status");

            entity.HasOne(d => d.Learner).WithMany(p => p.LearnerMasteries)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__LearnerMa__Learn__2DE6D218");

            entity.HasOne(d => d.Quest).WithMany(p => p.LearnerMasteries)
                .HasPrincipalKey(p => p.QuestId)
                .HasForeignKey(d => d.QuestId)
                .HasConstraintName("FK__LearnerMa__Quest__2CF2ADDF");
        });

        modelBuilder.Entity<LearnersCollaboration>(entity =>
        {
            entity.HasKey(e => new { e.LearnerId, e.QuestId }).HasName("PK__Learners__CCCDE5564F86EFE1");

            entity.ToTable("LearnersCollaboration");

            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.QuestId).HasColumnName("QuestID");
            entity.Property(e => e.CompletionStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("completion_status");

            entity.HasOne(d => d.Learner).WithMany(p => p.LearnersCollaborations)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__LearnersC__Learn__29221CFB");

            entity.HasOne(d => d.Quest).WithMany(p => p.LearnersCollaborations)
                .HasForeignKey(d => d.QuestId)
                .HasConstraintName("FK__LearnersC__Quest__2A164134");
        });

        modelBuilder.Entity<LearningActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__Learning__45F4A7F1AEFF5703");

            entity.ToTable("Learning_activities");

            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("activity_type");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.InstructionDetails)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instruction_details");
            entity.Property(e => e.MaxPoints).HasColumnName("Max_points");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

            entity.HasOne(d => d.Module).WithMany(p => p.LearningActivities)
                .HasForeignKey(d => new { d.ModuleId, d.CourseId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Learning_activit__5BE2A6F2");
        });

        modelBuilder.Entity<LearningGoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Learning__3214EC274D64754A");

            entity.ToTable("Learning_goal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasMany(d => d.Learners).WithMany(p => p.Goals)
                .UsingEntity<Dictionary<string, object>>(
                    "LearnersGoal",
                    r => r.HasOne<Learner>().WithMany()
                        .HasForeignKey("LearnerId")
                        .HasConstraintName("FK__LearnersG__Learn__04E4BC85"),
                    l => l.HasOne<LearningGoal>().WithMany()
                        .HasForeignKey("GoalId")
                        .HasConstraintName("FK__LearnersG__GoalI__03F0984C"),
                    j =>
                    {
                        j.HasKey("GoalId", "LearnerId").HasName("PK__Learners__3C3540FE56335704");
                        j.ToTable("LearnersGoals");
                        j.IndexerProperty<int>("GoalId").HasColumnName("GoalID");
                        j.IndexerProperty<int>("LearnerId").HasColumnName("LearnerID");
                    });
        });

        modelBuilder.Entity<LearningPath>(entity =>
        {
            entity.HasKey(e => e.PathId).HasName("PK__Learning__CD67DC3982E2DA3B");

            entity.ToTable("Learning_path");

            entity.Property(e => e.PathId).HasColumnName("PathID");
            entity.Property(e => e.AdaptiveRules)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adaptive_rules");
            entity.Property(e => e.CompletionStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("completion_status");
            entity.Property(e => e.CustomContent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("custom_content");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.PersonalizationProfile).WithMany(p => p.LearningPaths)
                .HasForeignKey(d => new { d.LearnerId, d.ProfileId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Learning_path__66603565");
        });

        modelBuilder.Entity<LearningPreference>(entity =>
        {
            entity.HasKey(e => e.LearnerId).HasName("PK__Learning__67ABFCFA0E63C0DE");

            entity.ToTable("LearningPreference");

            entity.Property(e => e.LearnerId)
                .ValueGeneratedNever()
                .HasColumnName("LearnerID");
            entity.Property(e => e.Preference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("preference");

            entity.HasOne(d => d.Learner).WithOne(p => p.LearningPreference)
                .HasForeignKey<LearningPreference>(d => d.LearnerId)
                .HasConstraintName("FK__LearningP__Learn__3B75D760");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => new { e.ModuleId, e.CourseId }).HasName("PK__Modules__47E6A09F8FAB8BA4");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ContentUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contentURL");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("difficulty");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Modules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Modules__CourseI__49C3F6B7");
        });

        modelBuilder.Entity<ModuleContent>(entity =>
        {
            entity.HasKey(e => new { e.ModuleId, e.CourseId, e.ContentType }).HasName("PK__ModuleCo__402E75DAC4BF57E2");

            entity.ToTable("ModuleContent");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ContentType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content_type");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleContents)
                .HasForeignKey(d => new { d.ModuleId, d.CourseId })
                .HasConstraintName("FK__ModuleContent__4F7CD00D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC2772205947");

            entity.ToTable("Notification");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.UrgencyLevel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("urgency_level");

            entity.HasMany(d => d.Learners).WithMany(p => p.Notifications)
                .UsingEntity<Dictionary<string, object>>(
                    "ReceivedNotification",
                    r => r.HasOne<Learner>().WithMany()
                        .HasForeignKey("LearnerId")
                        .HasConstraintName("FK__ReceivedN__Learn__1332DBDC"),
                    l => l.HasOne<Notification>().WithMany()
                        .HasForeignKey("NotificationId")
                        .HasConstraintName("FK__ReceivedN__Notif__123EB7A3"),
                    j =>
                    {
                        j.HasKey("NotificationId", "LearnerId").HasName("PK__Received__96B591FD251F8963");
                        j.ToTable("ReceivedNotification");
                        j.IndexerProperty<int>("NotificationId").HasColumnName("NotificationID");
                        j.IndexerProperty<int>("LearnerId").HasColumnName("LearnerID");
                    });
        });

        modelBuilder.Entity<Pathreview>(entity =>
        {
            entity.HasKey(e => e.PathId).HasName("PK__Pathrevi__CD67DC397034A608");

            entity.ToTable("Pathreview");

            entity.Property(e => e.PathId).HasColumnName("PathID");
            entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            entity.Property(e => e.Review)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("review");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Pathreviews)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pathrevie__Instr__6B24EA82");
        });

        modelBuilder.Entity<PersonalizationProfile>(entity =>
        {
            entity.HasKey(e => new { e.LearnerId, e.ProfileId }).HasName("PK__Personal__353B34724B011353");

            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.EmotionalState)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("emotional_state");
            entity.Property(e => e.PersonalityType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("personality_type");
            entity.Property(e => e.PreferedContentType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Prefered_content_type");

            entity.HasOne(d => d.Learner).WithMany(p => p.PersonalizationProfiles)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__Personali__Learn__3E52440B");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.QuestId).HasName("PK__Quest__B6619ACB0A8A239F");

            entity.ToTable("Quest");

            entity.Property(e => e.QuestId)
                .ValueGeneratedNever()
                .HasColumnName("QuestID");
            entity.Property(e => e.Criteria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("criteria");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("difficulty_level");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<QuestReward>(entity =>
        {
            entity.HasKey(e => new { e.RewardId, e.QuestId, e.LearnerId }).HasName("PK__QuestRew__D251A7C9F490BC4A");

            entity.ToTable("QuestReward");

            entity.Property(e => e.RewardId).HasColumnName("RewardID");
            entity.Property(e => e.QuestId).HasColumnName("QuestID");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.TimeEarned)
                .HasColumnType("datetime")
                .HasColumnName("Time_earned");

            entity.HasOne(d => d.Learner).WithMany(p => p.QuestRewards)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__QuestRewa__Learn__395884C4");

            entity.HasOne(d => d.Quest).WithMany(p => p.QuestRewards)
                .HasForeignKey(d => d.QuestId)
                .HasConstraintName("FK__QuestRewa__Quest__3864608B");

            entity.HasOne(d => d.Reward).WithMany(p => p.QuestRewards)
                .HasForeignKey(d => d.RewardId)
                .HasConstraintName("FK__QuestRewa__Rewar__37703C52");
        });

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => new { e.BoardId, e.LearnerId, e.CourseId }).HasName("PK__Ranking__C9D7F96C779E0510");

            entity.ToTable("Ranking");

            entity.Property(e => e.BoardId).HasColumnName("BoardID");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.TotalPoints).HasColumnName("total_points");

            entity.HasOne(d => d.Board).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK__Ranking__BoardID__7D439ABD");

            entity.HasOne(d => d.Course).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Ranking__CourseI__7F2BE32F");

            entity.HasOne(d => d.Learner).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__Ranking__Learner__7E37BEF6");
        });

        modelBuilder.Entity<Reward>(entity =>
        {
            entity.HasKey(e => e.RewardId).HasName("PK__Reward__8250159967EEE1CF");

            entity.ToTable("Reward");

            entity.Property(e => e.RewardId)
                .ValueGeneratedNever()
                .HasColumnName("RewardID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => new { e.LearnerId, e.Skill1 }).HasName("PK__Skills__C45BDEA57D85496C");

            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.Skill1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("skill");

            entity.HasOne(d => d.Learner).WithMany(p => p.Skills)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__Skills__LearnerI__38996AB5");
        });

        modelBuilder.Entity<SkillMastery>(entity =>
        {
            entity.HasKey(e => new { e.QuestId, e.Skill }).HasName("PK__Skill_Ma__1591B894E525C93A");

            entity.ToTable("Skill_Mastery");

            entity.HasIndex(e => e.QuestId, "UQ__Skill_Ma__B6619ACA368F6064").IsUnique();

            entity.Property(e => e.QuestId).HasColumnName("QuestID");
            entity.Property(e => e.Skill)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("skill");

            entity.HasOne(d => d.Quest).WithOne(p => p.SkillMastery)
                .HasForeignKey<SkillMastery>(d => d.QuestId)
                .HasConstraintName("FK__Skill_Mas__Quest__236943A5");
        });

        modelBuilder.Entity<SkillProgression>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SkillPro__3214EC27CFD054FF");

            entity.ToTable("SkillProgression");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.ProficiencyLevel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("proficiency_level");
            entity.Property(e => e.SkillName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("skill_name");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Skill).WithMany(p => p.SkillProgressions)
                .HasForeignKey(d => new { d.LearnerId, d.SkillName })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SkillProgression__17F790F9");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Survey__3214EC27A0E64281");

            entity.ToTable("Survey");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SurveyQuestion>(entity =>
        {
            entity.HasKey(e => new { e.SurveyId, e.Question }).HasName("PK__SurveyQu__23FB983B72F26366");

            entity.Property(e => e.SurveyId).HasColumnName("SurveyID");
            entity.Property(e => e.Question)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Survey).WithMany(p => p.SurveyQuestions)
                .HasForeignKey(d => d.SurveyId)
                .HasConstraintName("FK__SurveyQue__Surve__09A971A2");
        });

        modelBuilder.Entity<Takenassessment>(entity =>
        {
            entity.HasKey(e => new { e.AssessmentId, e.LearnerId }).HasName("PK__Takenass__8B5147F1FDBF0997");

            entity.ToTable("Takenassessment");

            entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");
            entity.Property(e => e.LearnerId).HasColumnName("LearnerID");
            entity.Property(e => e.ScoredPoint).HasColumnName("scoredPoint");

            entity.HasOne(d => d.Assessment).WithMany(p => p.Takenassessments)
                .HasForeignKey(d => d.AssessmentId)
                .HasConstraintName("FK__Takenasse__Asses__59063A47");

            entity.HasOne(d => d.Learner).WithMany(p => p.Takenassessments)
                .HasForeignKey(d => d.LearnerId)
                .HasConstraintName("FK__Takenasse__Learn__5812160E");
        });

        modelBuilder.Entity<TargetTrait>(entity =>
        {
            entity.HasKey(e => new { e.ModuleId, e.CourseId, e.Trait }).HasName("PK__Target_t__4E005E4C50307B4F");

            entity.ToTable("Target_traits");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Trait)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Module).WithMany(p => p.TargetTraits)
                .HasForeignKey(d => new { d.ModuleId, d.CourseId })
                .HasConstraintName("FK__Target_traits__4CA06362");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07950F0667");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534B32F2497").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProfilePicture).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

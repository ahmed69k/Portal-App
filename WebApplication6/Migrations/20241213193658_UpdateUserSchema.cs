using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication6.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    BadgeID = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    criteria = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Badge__1918237CE2ED6FF5", x => x.BadgeID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    learning_objective = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    credit_points = table.Column<int>(type: "int", nullable: true),
                    difficulty_level = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__C92D718769AE5F94", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    latest_qualification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    expertise_area = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Instruct__9D010B7BB682F73F", x => x.InstructorID);
                });

            migrationBuilder.CreateTable(
                name: "Leaderboard",
                columns: table => new
                {
                    BoardID = table.Column<int>(type: "int", nullable: false),
                    season = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leaderbo__F9646BD262AEC362", x => x.BoardID);
                });

            migrationBuilder.CreateTable(
                name: "Learning_goal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__3214EC274D64754A", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    message = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    urgency_level = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ReadStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3214EC2772205947", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    difficulty_level = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    criteria = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quest__B6619ACB0A8A239F", x => x.QuestID);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    RewardID = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reward__8250159967EEE1CF", x => x.RewardID);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Survey__3214EC27A0E64281", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LearnerId = table.Column<int>(type: "int", nullable: true),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC07950F0667", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrerequisite",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Prereq = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CoursePr__F8693C2CD4B9D900", x => new { x.CourseID, x.Prereq });
                    table.ForeignKey(
                        name: "FK__CoursePre__Cours__45F365D3",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK__CoursePre__Prere__46E78A0C",
                        column: x => x.Prereq,
                        principalTable: "Course",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    difficulty = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    contentURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modules__47E6A09F8FAB8BA4", x => new { x.ModuleID, x.CourseID });
                    table.ForeignKey(
                        name: "FK__Modules__CourseI__49C3F6B7",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pathreview",
                columns: table => new
                {
                    PathID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorID = table.Column<int>(type: "int", nullable: true),
                    review = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pathrevi__CD67DC397034A608", x => x.PathID);
                    table.ForeignKey(
                        name: "FK__Pathrevie__Instr__6B24EA82",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teaches",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teaches__F193DC635A4CB8CE", x => new { x.InstructorID, x.CourseID });
                    table.ForeignKey(
                        name: "FK__Teaches__CourseI__787EE5A0",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Teaches__Instruc__778AC167",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborative",
                columns: table => new
                {
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    max_num_participants = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Collabor__B6619ACBA711EE96", x => x.QuestID);
                    table.ForeignKey(
                        name: "FK__Collabora__Quest__2645B050",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill_Mastery",
                columns: table => new
                {
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skill_Ma__1591B894E525C93A", x => new { x.QuestID, x.skill });
                    table.UniqueConstraint("AK_Skill_Mastery_QuestID", x => x.QuestID);
                    table.ForeignKey(
                        name: "FK__Skill_Mas__Quest__236943A5",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SurveyQu__23FB983B72F26366", x => new { x.SurveyID, x.Question });
                    table.ForeignKey(
                        name: "FK__SurveyQue__Surve__09A971A2",
                        column: x => x.SurveyID,
                        principalTable: "Survey",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learner",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    country = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    cultural_background = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learner__67ABFCFA3C1358A3", x => x.LearnerID);
                    table.ForeignKey(
                        name: "FK_Learner_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    total_marks = table.Column<int>(type: "int", nullable: true),
                    passing_marks = table.Column<int>(type: "int", nullable: true),
                    criteria = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    weightage = table.Column<double>(type: "float", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__3214EC271B42907A", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Assessments__5535A963",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentLibrary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    metadata = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    content_URL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContentL__3214EC27C7CFFC8B", x => x.ID);
                    table.ForeignKey(
                        name: "FK__ContentLibrary__52593CB8",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discussion_forum",
                columns: table => new
                {
                    ForumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_active = table.Column<DateTime>(type: "datetime", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discussi__E210AC4FEBAF192A", x => x.ForumID);
                    table.ForeignKey(
                        name: "FK__Discussion_forum__30C33EC3",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learning_activities",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    activity_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    instruction_details = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Max_points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__45F4A7F1AEFF5703", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK__Learning_activit__5BE2A6F2",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleContent",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    content_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ModuleCo__402E75DAC4BF57E2", x => new { x.ModuleID, x.CourseID, x.content_type });
                    table.ForeignKey(
                        name: "FK__ModuleContent__4F7CD00D",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Target_traits",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    Trait = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Target_t__4E005E4C50307B4F", x => new { x.ModuleID, x.CourseID, x.Trait });
                    table.ForeignKey(
                        name: "FK__Target_traits__4CA06362",
                        columns: x => new { x.ModuleID, x.CourseID },
                        principalTable: "Modules",
                        principalColumns: new[] { "ModuleID", "CourseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    AchievementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    BadgeID = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    date_earned = table.Column<DateOnly>(type: "date", nullable: true),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Achievem__276330E0270D96D5", x => x.AchievementID);
                    table.ForeignKey(
                        name: "FK__Achieveme__Badge__1BC821DD",
                        column: x => x.BadgeID,
                        principalTable: "Badge",
                        principalColumn: "BadgeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Achieveme__Learn__1AD3FDA4",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course_enrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    completion_date = table.Column<DateOnly>(type: "date", nullable: true),
                    enrollment_date = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, defaultValue: "In Progress")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course_e__7F6877FB1FE42095", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK__Course_en__Cours__73BA3083",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Course_en__Learn__74AE54BC",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilledSurvey",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FilledSu__D89C33C72EB2C16B", x => new { x.SurveyID, x.Question, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__FilledSur__Learn__0D7A0286",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__FilledSurvey__0C85DE4D",
                        columns: x => new { x.SurveyID, x.Question },
                        principalTable: "SurveyQuestions",
                        principalColumns: new[] { "SurveyID", "Question" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerMastery",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    completion_status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LearnerM__CCCDE55661334F29", x => new { x.LearnerID, x.QuestID });
                    table.ForeignKey(
                        name: "FK__LearnerMa__Learn__2DE6D218",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnerMa__Quest__2CF2ADDF",
                        column: x => x.QuestID,
                        principalTable: "Skill_Mastery",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnersCollaboration",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    completion_status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learners__CCCDE5564F86EFE1", x => new { x.LearnerID, x.QuestID });
                    table.ForeignKey(
                        name: "FK__LearnersC__Learn__29221CFB",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnersC__Quest__2A164134",
                        column: x => x.QuestID,
                        principalTable: "Collaborative",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnersGoals",
                columns: table => new
                {
                    GoalID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learners__3C3540FE56335704", x => new { x.GoalID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__LearnersG__GoalI__03F0984C",
                        column: x => x.GoalID,
                        principalTable: "Learning_goal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnersG__Learn__04E4BC85",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPreference",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    preference = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__67ABFCFA0E63C0DE", x => x.LearnerID);
                    table.ForeignKey(
                        name: "FK__LearningP__Learn__3B75D760",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizationProfiles",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false),
                    Prefered_content_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    emotional_state = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    personality_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Personal__353B34724B011353", x => new { x.LearnerID, x.ProfileID });
                    table.ForeignKey(
                        name: "FK__Personali__Learn__3E52440B",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestReward",
                columns: table => new
                {
                    RewardID = table.Column<int>(type: "int", nullable: false),
                    QuestID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    Time_earned = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuestRew__D251A7C9F490BC4A", x => new { x.RewardID, x.QuestID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__QuestRewa__Learn__395884C4",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__QuestRewa__Quest__3864608B",
                        column: x => x.QuestID,
                        principalTable: "Quest",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__QuestRewa__Rewar__37703C52",
                        column: x => x.RewardID,
                        principalTable: "Reward",
                        principalColumn: "RewardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ranking",
                columns: table => new
                {
                    BoardID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    rank = table.Column<int>(type: "int", nullable: true),
                    total_points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ranking__C9D7F96C779E0510", x => new { x.BoardID, x.LearnerID, x.CourseID });
                    table.ForeignKey(
                        name: "FK__Ranking__BoardID__7D439ABD",
                        column: x => x.BoardID,
                        principalTable: "Leaderboard",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Ranking__CourseI__7F2BE32F",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Ranking__Learner__7E37BEF6",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedNotification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Received__96B591FD251F8963", x => new { x.NotificationID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__ReceivedN__Learn__1332DBDC",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ReceivedN__Notif__123EB7A3",
                        column: x => x.NotificationID,
                        principalTable: "Notification",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skills__C45BDEA57D85496C", x => new { x.LearnerID, x.skill });
                    table.ForeignKey(
                        name: "FK__Skills__LearnerI__38996AB5",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Takenassessment",
                columns: table => new
                {
                    AssessmentID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    scoredPoint = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Takenass__8B5147F1FDBF0997", x => new { x.AssessmentID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__Takenasse__Asses__59063A47",
                        column: x => x.AssessmentID,
                        principalTable: "Assessments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Takenasse__Learn__5812160E",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerDiscussion",
                columns: table => new
                {
                    ForumID = table.Column<int>(type: "int", nullable: false),
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    Post = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LearnerD__546A1380C98B1B35", x => new { x.ForumID, x.LearnerID });
                    table.ForeignKey(
                        name: "FK__LearnerDi__Forum__339FAB6E",
                        column: x => x.ForumID,
                        principalTable: "Discussion_forum",
                        principalColumn: "ForumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__LearnerDi__Learn__3493CFA7",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emotional_feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    activityID = table.Column<int>(type: "int", nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    emotional_state = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Emotiona__6A4BEDF601950633", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Emotional__Learn__628FA481",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Emotional__activ__6383C8BA",
                        column: x => x.activityID,
                        principalTable: "Learning_activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interaction_log",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false),
                    activity_ID = table.Column<int>(type: "int", nullable: true),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    action_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Interact__5E5499A82D79211A", x => x.LogID);
                    table.ForeignKey(
                        name: "FK__Interacti__Learn__5FB337D6",
                        column: x => x.LearnerID,
                        principalTable: "Learner",
                        principalColumn: "LearnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Interacti__activ__5EBF139D",
                        column: x => x.activity_ID,
                        principalTable: "Learning_activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthCondition",
                columns: table => new
                {
                    LearnerID = table.Column<int>(type: "int", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HealthCo__930320B08BD1B855", x => new { x.LearnerID, x.ProfileID, x.condition });
                    table.ForeignKey(
                        name: "FK__HealthCondition__412EB0B6",
                        columns: x => new { x.LearnerID, x.ProfileID },
                        principalTable: "PersonalizationProfiles",
                        principalColumns: new[] { "LearnerID", "ProfileID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learning_path",
                columns: table => new
                {
                    PathID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    ProfileID = table.Column<int>(type: "int", nullable: true),
                    completion_status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    custom_content = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    adaptive_rules = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__CD67DC3982E2DA3B", x => x.PathID);
                    table.ForeignKey(
                        name: "FK__Learning_path__66603565",
                        columns: x => new { x.LearnerID, x.ProfileID },
                        principalTable: "PersonalizationProfiles",
                        principalColumns: new[] { "LearnerID", "ProfileID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillProgression",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    proficiency_level = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LearnerID = table.Column<int>(type: "int", nullable: true),
                    skill_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SkillPro__3214EC27CFD054FF", x => x.ID);
                    table.ForeignKey(
                        name: "FK__SkillProgression__17F790F9",
                        columns: x => new { x.LearnerID, x.skill_name },
                        principalTable: "Skills",
                        principalColumns: new[] { "LearnerID", "skill" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emotionalfeedback_review",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    review = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Emotiona__C39BFD41475B7430", x => new { x.FeedbackID, x.InstructorID });
                    table.ForeignKey(
                        name: "FK__Emotional__Feedb__6E01572D",
                        column: x => x.FeedbackID,
                        principalTable: "Emotional_feedback",
                        principalColumn: "FeedbackID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Emotional__Instr__6EF57B66",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_BadgeID",
                table: "Achievement",
                column: "BadgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_LearnerID",
                table: "Achievement",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ModuleID_CourseID",
                table: "Assessments",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentLibrary_ModuleID_CourseID",
                table: "ContentLibrary",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Course_enrollment_CourseID",
                table: "Course_enrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_enrollment_LearnerID",
                table: "Course_enrollment",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrerequisite_Prereq",
                table: "CoursePrerequisite",
                column: "Prereq");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_forum_ModuleID_CourseID",
                table: "Discussion_forum",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Emotional_feedback_activityID",
                table: "Emotional_feedback",
                column: "activityID");

            migrationBuilder.CreateIndex(
                name: "IX_Emotional_feedback_LearnerID",
                table: "Emotional_feedback",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Emotionalfeedback_review_InstructorID",
                table: "Emotionalfeedback_review",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_FilledSurvey_LearnerID",
                table: "FilledSurvey",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Interaction_log_activity_ID",
                table: "Interaction_log",
                column: "activity_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Interaction_log_LearnerID",
                table: "Interaction_log",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Learner_UserId1",
                table: "Learner",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerDiscussion_LearnerID",
                table: "LearnerDiscussion",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerMastery_QuestID",
                table: "LearnerMastery",
                column: "QuestID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnersCollaboration_QuestID",
                table: "LearnersCollaboration",
                column: "QuestID");

            migrationBuilder.CreateIndex(
                name: "IX_LearnersGoals_LearnerID",
                table: "LearnersGoals",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Learning_activities_ModuleID_CourseID",
                table: "Learning_activities",
                columns: new[] { "ModuleID", "CourseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Learning_path_LearnerID_ProfileID",
                table: "Learning_path",
                columns: new[] { "LearnerID", "ProfileID" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseID",
                table: "Modules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Pathreview_InstructorID",
                table: "Pathreview",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestReward_LearnerID",
                table: "QuestReward",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestReward_QuestID",
                table: "QuestReward",
                column: "QuestID");

            migrationBuilder.CreateIndex(
                name: "IX_Ranking_CourseID",
                table: "Ranking",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Ranking_LearnerID",
                table: "Ranking",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedNotification_LearnerID",
                table: "ReceivedNotification",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "UQ__Skill_Ma__B6619ACA368F6064",
                table: "Skill_Mastery",
                column: "QuestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillProgression_LearnerID_skill_name",
                table: "SkillProgression",
                columns: new[] { "LearnerID", "skill_name" });

            migrationBuilder.CreateIndex(
                name: "IX_Takenassessment_LearnerID",
                table: "Takenassessment",
                column: "LearnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Teaches_CourseID",
                table: "Teaches",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534B32F2497",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "ContentLibrary");

            migrationBuilder.DropTable(
                name: "Course_enrollment");

            migrationBuilder.DropTable(
                name: "CoursePrerequisite");

            migrationBuilder.DropTable(
                name: "Emotionalfeedback_review");

            migrationBuilder.DropTable(
                name: "FilledSurvey");

            migrationBuilder.DropTable(
                name: "HealthCondition");

            migrationBuilder.DropTable(
                name: "Interaction_log");

            migrationBuilder.DropTable(
                name: "LearnerDiscussion");

            migrationBuilder.DropTable(
                name: "LearnerMastery");

            migrationBuilder.DropTable(
                name: "LearnersCollaboration");

            migrationBuilder.DropTable(
                name: "LearnersGoals");

            migrationBuilder.DropTable(
                name: "Learning_path");

            migrationBuilder.DropTable(
                name: "LearningPreference");

            migrationBuilder.DropTable(
                name: "ModuleContent");

            migrationBuilder.DropTable(
                name: "Pathreview");

            migrationBuilder.DropTable(
                name: "QuestReward");

            migrationBuilder.DropTable(
                name: "Ranking");

            migrationBuilder.DropTable(
                name: "ReceivedNotification");

            migrationBuilder.DropTable(
                name: "SkillProgression");

            migrationBuilder.DropTable(
                name: "Takenassessment");

            migrationBuilder.DropTable(
                name: "Target_traits");

            migrationBuilder.DropTable(
                name: "Teaches");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropTable(
                name: "Emotional_feedback");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Discussion_forum");

            migrationBuilder.DropTable(
                name: "Skill_Mastery");

            migrationBuilder.DropTable(
                name: "Collaborative");

            migrationBuilder.DropTable(
                name: "Learning_goal");

            migrationBuilder.DropTable(
                name: "PersonalizationProfiles");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "Leaderboard");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "Learning_activities");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "Learner");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}

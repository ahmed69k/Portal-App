using System;
using System.Collections.Generic;

namespace WebApplication6.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? ProfilePicture { get; set; }
    public int? LearnerId { get; set; } // Nullable for roles that aren't learners
    public int? InstructorId { get; set; } // Nullable for roles that aren't instructors
}

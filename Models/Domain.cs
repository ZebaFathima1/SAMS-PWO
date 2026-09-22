using System.ComponentModel.DataAnnotations;

namespace StudentAcademicManagementSystem.Models;

public abstract class Person
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    public abstract string GetRole();
}

public class Student : Person
{
    [Required] public string RollNumber { get; set; } = string.Empty;
    [Required] public string Department { get; set; } = string.Empty;
    [Range(1, 12)] public int Semester { get; set; }
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = new(2004, 1, 1);
    public string Gender { get; set; } = "Prefer not to say";
    public string Address { get; set; } = string.Empty;
    public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    public ICollection<Attendance> AttendanceRecords { get; set; } = new List<Attendance>();
    public override string GetRole() => "Student";
}

public abstract class User
{
    public int Id { get; set; }
    [Required] public string Username { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public abstract string GetRole();
}
public class Admin : User { public override string GetRole() => "Admin"; }
public class StudentUser : User { public int StudentId { get; set; } public Student? Student { get; set; } public override string GetRole() => "Student"; }

public class Subject
{
    public int Id { get; set; }
    [Required] public string SubjectCode { get; set; } = string.Empty;
    [Required] public string SubjectName { get; set; } = string.Empty;
    [Range(1, 10)] public int Credits { get; set; } = 3;
    [Range(1, 12)] public int Semester { get; set; } = 1;
    public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    public ICollection<Attendance> AttendanceRecords { get; set; } = new List<Attendance>();
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
public class Mark
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    [Range(0, 40)] public decimal InternalMarks { get; set; }
    [Range(0, 60)] public decimal ExternalMarks { get; set; }
    public decimal TotalMarks => InternalMarks + ExternalMarks;
    public string Grade => Services.GradeService.Calculate(TotalMarks);
}
public class Attendance
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    [Range(0, 1000)] public int ConductedClasses { get; set; }
    [Range(0, 1000)] public int AttendedClasses { get; set; }
    public decimal AttendancePercentage => ConductedClasses == 0 ? 0 : Math.Round((decimal)AttendedClasses / ConductedClasses * 100, 1);
}
public class Assignment
{
    public int Id { get; set; }
    [Required] public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
    public string Status { get; set; } = "Pending";
}

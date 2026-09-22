using StudentAcademicManagementSystem.Models;
namespace StudentAcademicManagementSystem.Data;
public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Students.Any()) return;
        var subjects = Enumerable.Range(1, 6).Select(i => new Subject { SubjectCode = $"CS-{100 + i}", SubjectName = new[] { "Object Oriented Programming", "Database Systems", "Web Engineering", "Data Structures", "Software Design", "Computer Networks" }[i - 1], Credits = 3, Semester = 2 }).ToList();
        db.Subjects.AddRange(subjects);
        var students = Enumerable.Range(1, 10).Select(i => new Student { RollNumber = $"SAMS2026{i:000}", FullName = new[] { "Aarav Mehta", "Diya Shah", "Kabir Rao", "Anaya Iyer", "Vivaan Nair", "Myra Menon", "Arjun Das", "Sara Khan", "Rohan Patel", "Ishita Roy" }[i - 1], Email = $"student{i}@sams.edu", Department = i % 2 == 0 ? "Information Technology" : "Computer Science", Semester = 2, Phone = $"90000000{i:00}" }).ToList();
        db.Students.AddRange(students); db.SaveChanges();
        db.Admins.Add(new Admin { Username = "admin", Password = "Admin@123" });
        db.StudentUsers.Add(new StudentUser { Username = "student", Password = "Student@123", StudentId = students[0].Id });
        foreach (var student in students) foreach (var subject in subjects)
        { db.Marks.Add(new Mark { StudentId = student.Id, SubjectId = subject.Id, InternalMarks = 22 + student.Id % 10, ExternalMarks = 42 + subject.Id % 12 }); db.Attendance.Add(new Attendance { StudentId = student.Id, SubjectId = subject.Id, ConductedClasses = 40, AttendedClasses = 27 + (student.Id + subject.Id) % 13 }); }
        db.Assignments.AddRange(subjects.Select((s, i) => new Assignment { SubjectId = s.Id, Title = $"{s.SubjectName} Case Study", Description = "Prepare a concise academic case study.", DueDate = DateTime.Today.AddDays(i - 2), Status = i % 3 == 0 ? "Completed" : i % 3 == 1 ? "Submitted" : "Pending" }));
        db.SaveChanges();
    }
}

using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
namespace StudentAcademicManagementSystem.Services;
public class ReportService
{
    private readonly ApplicationDbContext db;
    public ReportService(ApplicationDbContext db) => this.db = db;
    public async Task<string?> BuildStudentReportAsync(int id)
    {
        var student = await db.Students.Include(s => s.Marks).ThenInclude(m => m.Subject).Include(s => s.AttendanceRecords).ThenInclude(a => a.Subject).FirstOrDefaultAsync(s => s.Id == id);
        if (student is null) return null;
        var text = new StringBuilder($"SAMS ACADEMIC SUMMARY\n{student.FullName} | {student.RollNumber}\n{student.Department}, Semester {student.Semester}\n\nMARKS\n");
        foreach (var mark in student.Marks) text.AppendLine($"{mark.Subject?.SubjectName}: {mark.TotalMarks}/100 ({mark.Grade})");
        text.AppendLine("\nATTENDANCE\n");
        foreach (var record in student.AttendanceRecords) text.AppendLine($"{record.Subject?.SubjectName}: {record.AttendancePercentage}%");
        return text.ToString();
    }
}

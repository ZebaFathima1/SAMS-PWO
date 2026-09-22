using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
using StudentAcademicManagementSystem.Services;
[Authorize] public class ReportsController(ApplicationDbContext db, ReportService reports) : Controller
{
    public async Task<IActionResult> Index() { var query = db.Students.Include(s => s.Marks).Include(s => s.AttendanceRecords).AsQueryable(); if (User.IsInRole("Student")) { var studentId = int.Parse(User.FindFirst("StudentId")!.Value); query = query.Where(s => s.Id == studentId); } return View(await query.OrderBy(s => s.FullName).ToListAsync()); }
    public async Task<IActionResult> Export(int id) { var content = await reports.BuildStudentReportAsync(id); if (content is null) return NotFound(); return File(System.Text.Encoding.UTF8.GetBytes(content), "text/plain", $"SAMS-{id}-academic-summary.txt"); }
}

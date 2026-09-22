using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
namespace StudentAcademicManagementSystem.Controllers;
[Authorize(Roles = "Admin")] public class DashboardController(ApplicationDbContext db) : Controller
{
 public async Task<IActionResult> Index() { ViewBag.Students = await db.Students.CountAsync(); ViewBag.Subjects = await db.Subjects.CountAsync(); ViewBag.Assignments = await db.Assignments.CountAsync(); var marks = await db.Marks.Select(x => new { x.InternalMarks, x.ExternalMarks }).ToListAsync(); var attendance = await db.Attendance.Select(x => new { x.StudentId, x.AttendedClasses, x.ConductedClasses }).ToListAsync(); ViewBag.AverageMarks = marks.Count == 0 ? 0 : marks.Average(x => (double)x.InternalMarks + (double)x.ExternalMarks); ViewBag.AverageAttendance = attendance.Count == 0 ? 0 : attendance.Average(x => x.ConductedClasses == 0 ? 0 : (double)x.AttendedClasses / x.ConductedClasses * 100); ViewBag.Warnings = attendance.GroupBy(x => x.StudentId).Count(group => group.Average(x => x.ConductedClasses == 0 ? 0 : (double)x.AttendedClasses / x.ConductedClasses * 100) < 75); ViewBag.Recent = await db.Students.OrderByDescending(s => s.Id).Take(5).ToListAsync(); return View(); }
}

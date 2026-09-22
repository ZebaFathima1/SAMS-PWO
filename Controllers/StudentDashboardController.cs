using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
[Authorize(Roles = "Student")] public class StudentDashboardController(ApplicationDbContext db) : Controller
{ public async Task<IActionResult> Index() { var id = int.Parse(User.FindFirst("StudentId")!.Value); var student = await db.Students.Include(s => s.Marks).Include(s => s.AttendanceRecords).Include(s => s.Marks).ThenInclude(m => m.Subject).FirstAsync(s => s.Id == id); ViewBag.Student = student; ViewBag.Assignments = await db.Assignments.Include(a => a.Subject).Where(a => a.Status == "Pending").ToListAsync(); return View(); } }

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
using StudentAcademicManagementSystem.Models;
[Authorize] public class AttendanceController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index() { var query = db.Attendance.Include(a => a.Student).Include(a => a.Subject).AsQueryable(); if (User.IsInRole("Student")) { var studentId = int.Parse(User.FindFirst("StudentId")!.Value); query = query.Where(a => a.StudentId == studentId); } return View(await query.OrderByDescending(a => a.Id).ToListAsync()); }
    public async Task<IActionResult> Create() { await LoadOptions(); return View(new Attendance()); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Attendance attendance) { ValidateClasses(attendance); if (ModelState.IsValid) { db.Attendance.Add(attendance); await db.SaveChangesAsync(); TempData["Message"] = "Attendance saved successfully."; return RedirectToAction(nameof(Index)); } await LoadOptions(); return View(attendance); }
    public async Task<IActionResult> Edit(int id) { var attendance = await db.Attendance.FindAsync(id); if (attendance is null) return NotFound(); await LoadOptions(); return View(attendance); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(Attendance attendance) { ValidateClasses(attendance); if (ModelState.IsValid) { db.Attendance.Update(attendance); await db.SaveChangesAsync(); TempData["Message"] = "Attendance updated successfully."; return RedirectToAction(nameof(Index)); } await LoadOptions(); return View(attendance); }
    public async Task<IActionResult> Delete(int id) { var attendance = await db.Attendance.FindAsync(id); if (attendance is not null) { db.Attendance.Remove(attendance); await db.SaveChangesAsync(); TempData["Message"] = "Attendance deleted."; } return RedirectToAction(nameof(Index)); }
    private void ValidateClasses(Attendance value) { if (value.AttendedClasses > value.ConductedClasses) ModelState.AddModelError(nameof(value.AttendedClasses), "Attended classes cannot exceed conducted classes."); }
    private async Task LoadOptions() { ViewBag.Students = await db.Students.OrderBy(s => s.FullName).ToListAsync(); ViewBag.Subjects = await db.Subjects.OrderBy(s => s.SubjectName).ToListAsync(); }
}

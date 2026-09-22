using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
using StudentAcademicManagementSystem.Models;
[Authorize] public class MarksController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index() { var query = db.Marks.Include(m => m.Student).Include(m => m.Subject).AsQueryable(); if (User.IsInRole("Student")) { var studentId = int.Parse(User.FindFirst("StudentId")!.Value); query = query.Where(m => m.StudentId == studentId); } return View(await query.OrderByDescending(m => m.Id).ToListAsync()); }
    public async Task<IActionResult> Create() { await LoadOptions(); return View(new Mark()); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Mark mark) { if (ModelState.IsValid) { db.Marks.Add(mark); await db.SaveChangesAsync(); TempData["Message"] = "Marks saved successfully."; return RedirectToAction(nameof(Index)); } await LoadOptions(); return View(mark); }
    public async Task<IActionResult> Edit(int id) { var mark = await db.Marks.FindAsync(id); if (mark is null) return NotFound(); await LoadOptions(); return View(mark); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(Mark mark) { if (ModelState.IsValid) { db.Marks.Update(mark); await db.SaveChangesAsync(); TempData["Message"] = "Marks updated successfully."; return RedirectToAction(nameof(Index)); } await LoadOptions(); return View(mark); }
    public async Task<IActionResult> Delete(int id) { var mark = await db.Marks.FindAsync(id); if (mark is not null) { db.Marks.Remove(mark); await db.SaveChangesAsync(); TempData["Message"] = "Marks deleted."; } return RedirectToAction(nameof(Index)); }
    private async Task LoadOptions() { ViewBag.Students = await db.Students.OrderBy(s => s.FullName).ToListAsync(); ViewBag.Subjects = await db.Subjects.OrderBy(s => s.SubjectName).ToListAsync(); }
}

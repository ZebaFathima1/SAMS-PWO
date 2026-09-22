using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
using StudentAcademicManagementSystem.Models;
[Authorize] public class AssignmentsController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? status) { var query = db.Assignments.Include(a => a.Subject).AsQueryable(); if (!string.IsNullOrWhiteSpace(status)) query = query.Where(a => a.Status == status); ViewBag.Status = status; return View(await query.OrderBy(a => a.DueDate).ToListAsync()); }
    [Authorize(Roles = "Admin")] public async Task<IActionResult> Create() { await LoadOptions(); return View(new Assignment()); }
    [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken] public async Task<IActionResult> Create(Assignment assignment) { if (ModelState.IsValid) { db.Assignments.Add(assignment); await db.SaveChangesAsync(); TempData["Message"] = "Assignment added successfully."; return RedirectToAction(nameof(Index)); } await LoadOptions(); return View(assignment); }
    [Authorize(Roles = "Admin")] public async Task<IActionResult> Edit(int id) { var assignment = await db.Assignments.FindAsync(id); if (assignment is null) return NotFound(); await LoadOptions(); return View(assignment); }
    [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken] public async Task<IActionResult> Edit(Assignment assignment) { if (ModelState.IsValid) { db.Assignments.Update(assignment); await db.SaveChangesAsync(); TempData["Message"] = "Assignment updated successfully."; return RedirectToAction(nameof(Index)); } await LoadOptions(); return View(assignment); }
    [Authorize(Roles = "Admin")] public async Task<IActionResult> Delete(int id) { var assignment = await db.Assignments.FindAsync(id); if (assignment is not null) { db.Assignments.Remove(assignment); await db.SaveChangesAsync(); TempData["Message"] = "Assignment deleted."; } return RedirectToAction(nameof(Index)); }
    private async Task LoadOptions() => ViewBag.Subjects = await db.Subjects.OrderBy(s => s.SubjectName).ToListAsync();
}

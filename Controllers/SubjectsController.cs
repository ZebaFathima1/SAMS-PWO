using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
using StudentAcademicManagementSystem.Models;
[Authorize(Roles = "Admin")] public class SubjectsController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search) { var query = db.Subjects.AsQueryable(); if (!string.IsNullOrWhiteSpace(search)) query = query.Where(s => s.SubjectCode.Contains(search) || s.SubjectName.Contains(search)); ViewBag.Search = search; return View(await query.OrderBy(s => s.SubjectCode).ToListAsync()); }
    public IActionResult Create() => View(new Subject());
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Subject subject) { if (!ModelState.IsValid) return View(subject); db.Subjects.Add(subject); await db.SaveChangesAsync(); TempData["Message"] = "Subject added successfully."; return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Edit(int id) { var subject = await db.Subjects.FindAsync(id); return subject is null ? NotFound() : View(subject); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(Subject subject) { if (!ModelState.IsValid) return View(subject); db.Subjects.Update(subject); await db.SaveChangesAsync(); TempData["Message"] = "Subject updated successfully."; return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Delete(int id) { var subject = await db.Subjects.FindAsync(id); if (subject is not null) { db.Subjects.Remove(subject); await db.SaveChangesAsync(); TempData["Message"] = "Subject deleted."; } return RedirectToAction(nameof(Index)); }
}

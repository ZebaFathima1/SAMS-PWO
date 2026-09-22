using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Data;
using StudentAcademicManagementSystem.Models;
using StudentAcademicManagementSystem.Services;
[Authorize(Roles = "Admin")] public class StudentsController(ApplicationDbContext db) : Controller
{
 public async Task<IActionResult> Index(string? search, string? department) { var q = db.Students.AsQueryable(); if (!string.IsNullOrWhiteSpace(search)) q = q.Where(s => s.FullName.Contains(search) || s.RollNumber.Contains(search)); if (!string.IsNullOrWhiteSpace(department)) q = q.Where(s => s.Department == department); ViewBag.Departments = await db.Students.Select(s => s.Department).Distinct().ToListAsync(); ViewBag.Search = search; return View(await q.OrderBy(s => s.FullName).ToListAsync()); }
 public IActionResult Create() => View(new Student());
 [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Student student) { if (!ModelState.IsValid) return View(student); db.Students.Add(student); await db.SaveChangesAsync(); TempData["Message"] = "Student added successfully."; return RedirectToAction(nameof(Index)); }
 public async Task<IActionResult> Details(int id) { var student = await db.Students.Include(s => s.Marks).ThenInclude(m => m.Subject).Include(s => s.AttendanceRecords).ThenInclude(a => a.Subject).FirstOrDefaultAsync(s => s.Id == id); return student is null ? NotFound() : View(student); }
 public async Task<IActionResult> Delete(int id) { var student = await db.Students.FindAsync(id); if (student is not null) { db.Students.Remove(student); await db.SaveChangesAsync(); TempData["Message"] = "Student removed."; } return RedirectToAction(nameof(Index)); }
}

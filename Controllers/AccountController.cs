using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentAcademicManagementSystem.Data;
namespace StudentAcademicManagementSystem.Controllers;
public class AccountController(ApplicationDbContext db) : Controller
{
    [HttpGet] public IActionResult Login() => View();
    [HttpPost] public async Task<IActionResult> Login(string username, string password)
    {
        var admin = db.Admins.FirstOrDefault(x => x.Username == username && x.Password == password);
        var student = db.StudentUsers.FirstOrDefault(x => x.Username == username && x.Password == password);
        if (admin is not null) return await SignInAndRedirect(admin.Username, "Admin", "/Dashboard");
        if (student is not null) return await SignInAndRedirect(student.Username, "Student", "/StudentDashboard", student.StudentId);
        ViewBag.Error = "The username or password is incorrect."; return View();
    }
    private async Task<IActionResult> SignInAndRedirect(string name, string role, string path, int? studentId = null)
    { var claims = new List<Claim> { new(ClaimTypes.Name, name), new(ClaimTypes.Role, role) }; if (studentId.HasValue) claims.Add(new("StudentId", studentId.Value.ToString())); await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))); return Redirect(path); }
    public async Task<IActionResult> Logout() { await HttpContext.SignOutAsync(); return RedirectToAction("Login"); }
}

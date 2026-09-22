using Microsoft.EntityFrameworkCore;
using StudentAcademicManagementSystem.Models;

namespace StudentAcademicManagementSystem.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<StudentUser> StudentUsers => Set<StudentUser>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Mark> Marks => Set<Mark>();
    public DbSet<Attendance> Attendance => Set<Attendance>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mark>().Ignore(m => m.TotalMarks).Ignore(m => m.Grade);
        modelBuilder.Entity<Attendance>().Ignore(a => a.AttendancePercentage);
        modelBuilder.Entity<StudentUser>().HasOne(x => x.Student).WithMany().HasForeignKey(x => x.StudentId);
        modelBuilder.Entity<Mark>().HasOne(x => x.Student).WithMany(x => x.Marks).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Mark>().HasOne(x => x.Subject).WithMany(x => x.Marks).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Attendance>().HasOne(x => x.Student).WithMany(x => x.AttendanceRecords).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Attendance>().HasOne(x => x.Subject).WithMany(x => x.AttendanceRecords).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Assignment>().HasOne(x => x.Subject).WithMany(x => x.Assignments).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Cascade);
    }
}

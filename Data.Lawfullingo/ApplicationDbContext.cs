using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course_Class>()
            .HasOne(c => c.Teachers)
            .WithMany()
            .HasForeignKey(c => c.teacher_id)
            .OnDelete(DeleteBehavior.Restrict); // or .SetNull

        modelBuilder.Entity<Course_Class>()
            .HasOne(c => c.course)
            .WithMany()
            .HasForeignKey(c => c.course_id)
            .OnDelete(DeleteBehavior.Restrict); // or .SetNull
    }
   
    public DbSet<Category> Categories { get; set; }
    public DbSet<Class_Videos> Class_Videos { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Course_Class> Courses_Class { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Teachers> Teachers { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<TeacherType> TeacherTypes { get; set; }
    public DbSet<OTP> OTPs { get; set; }
}
    
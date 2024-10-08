using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_leaening_System.Models
{
    public class MyData: IdentityDbContext<ApplicationUser>
    {
        public MyData(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Certificate>? Certificates { get; set; }
        public DbSet<Content>? Content { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Instructor>? Instructors { get; set; }
        public DbSet<Quiz>? Quizs { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<TheQuizzes>? TheQuizes { get; set; }
    }
}

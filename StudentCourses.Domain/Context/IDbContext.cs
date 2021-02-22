using Microsoft.EntityFrameworkCore;
using StudentCourses.Domain.Entities;

namespace StudentCourses.Domain.Context
{
    public interface IDbContext
    {
        int SaveChanges();

        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Course> Courses { get; set; }

    }

    
}
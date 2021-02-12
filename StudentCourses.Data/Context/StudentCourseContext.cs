using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Models
{
    public class StudentCourseContext:DbContext
    {
        public StudentCourseContext(DbContextOptions<StudentCourseContext> options)
            :base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

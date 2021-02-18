using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCourses.Domain;
using StudentCourses.Domain.Entities;
using StudentCourses.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Models
{
    public class StudentCourseContext : IdentityDbContext<SiteUser>, IDbContext
    {
        public StudentCourseContext(DbContextOptions<StudentCourseContext> options)
            : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

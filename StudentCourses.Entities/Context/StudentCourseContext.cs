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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            string deanRoleId = Guid.NewGuid().ToString();
            string deanUserId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = deanRoleId, Name = "Deans", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "DEANS" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Students", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "STUDENTS" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Teachers", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "TEACHERS" }
                );

            builder.Entity<IdentityUser>().HasData(
                new SiteUser { Id = deanUserId, Email = "dean@email.com", UserName = "dean@email.com", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() },
                new SiteUser { Id = Guid.NewGuid().ToString(), Email = "student@email.com", UserName = "student@email.com", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() },
                new SiteUser { Id = Guid.NewGuid().ToString(), Email = "teacher@email.com", UserName = "teacher@email.com", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() }
                );

            //add each to their own roles
            base.OnModelCreating(builder);
        }
    }
}

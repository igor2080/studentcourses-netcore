using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;
using StudentCourses.Domain;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using StudentCourses.Models;
using StudentCourses.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<StudentCourseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IService<CourseModel>, CourseService>();
            services.AddScoped<IRepository<Student>, StudentRepository>();
            services.AddScoped<IService<StudentModel>, StudentService>();
            services.AddScoped<IRepository<Group>, GroupRepository>();
            services.AddScoped<IService<GroupModel>, GroupService>();
            services.AddScoped<IDbContext>(x => x.GetService<StudentCourseContext>());
            services.AddDefaultIdentity<SiteUser>(options =>
            {
                options.Password = new PasswordOptions() { RequireDigit = false, RequiredLength = 4, RequiredUniqueChars = 0, RequireLowercase = false, RequireNonAlphanumeric = false, RequireUppercase = false };
            })
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<StudentCourseContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentCourseContext context, UserManager<SiteUser> userManager, RoleManager<IdentityRole> rolemgr)
        {
            SeedData(context, userManager, rolemgr);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void SeedData(StudentCourseContext context, UserManager<SiteUser> userManager, RoleManager<IdentityRole> rolemgr)
        {
	        if (context == null)
	        {
		        throw new ArgumentNullException(nameof(context));
	        }

	        if (userManager == null)
	        {
		        throw new ArgumentNullException(nameof(userManager));
	        }

	        if (rolemgr == null)
	        {
		        throw new ArgumentNullException(nameof(rolemgr));
	        }

	        context.Database.Migrate();

            var user = new SiteUser
	        {
		        Email = "eugenkhudoliiv@gmail.com",
		        UserName = "shikigami"
	        };

	        var adminRole = new IdentityRole("Deans");

            if (context.Users.FirstOrDefault(x => x.Email == user.Email) == null)
	        {
		        context.Users.Add(user);
		        context.SaveChanges();
			}
            else
            {
	            user = context.Users.FirstOrDefault(x => x.Email == user.Email);
            }

            if (!rolemgr.Roles.Any())
            {
	            rolemgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (context.UserRoles.Count(_ => true) == 0)
            {
	            userManager.AddToRoleAsync(user, adminRole.Name).GetAwaiter().GetResult();
                context.SaveChanges();
	        }
        }
    }
}

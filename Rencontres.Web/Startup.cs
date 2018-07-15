using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rencontres.Metier.Infrastructure;

namespace Rencontres.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<RencontresContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("RencontresDb")));

            services.AddIdentity<RencontreUser, RencontreRole>(options => {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<RencontresContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
               
            });
        }
        public static void SeedRoles(RoleManager<RencontreRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RencontreRole.Participant).Result)
            {
                var role = new RencontreRole(RencontreRole.Participant);

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(RencontreRole.Administrateur).Result)
            {
                var role = new RencontreRole(RencontreRole.Administrateur);
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<RencontreUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new RencontreUser
                {
                    UserName = "admin",
                    Email = "guillaume.jay@gmail.com"
                };
                IdentityResult result = userManager.CreateAsync(user, $"admin{DateTime.Now:yyMMdd}").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        RencontreRole.Administrateur).Wait();
                }
            }


        }
    }
}

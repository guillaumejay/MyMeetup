using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using ElmahCore;
using ElmahCore.Mvc;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMeetup.Web.Infrastructure;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Infrastructure.DataContexts;

namespace MyMeetup.Web
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
            services.AddLocalization();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddDbContext<MyMeetupContext, MyMeetupSqlServerContext>(options  =>
            //    options.UseSqlServer(
            //      Configuration.GetConnectionString("MyMeetupDb")));
            services.AddDbContext<MyMeetupContext, MyMeetupSqlLiteContext>(options =>
                    options.UseSqlite(
                        Configuration.GetConnectionString("MyMeetupDb")));

            services.AddIdentity<MyMeetupUser, MyMeetupRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<MyMeetupContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
           // services.AddElmah(options => options.Path = "erreurs");
            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/AppData"; options.Path = "erreurs"; 
            });
            services.AddScoped<MyMeetupDomain>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<TelemetryClient>();

            TelemetryConfiguration.Active.TelemetryInitializers.Add(new MyTelemetryInitializer(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<MyMeetupUser> userManager,
            RoleManager<MyMeetupRole> roleManager, MyMeetupContext context)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
             //   app.UseDeveloperExceptionPage();
              //  app.UseDatabaseErrorPage();
               app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
          // app.UseDeveloperExceptionPage();
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("fr"),
                new CultureInfo("fr-FR"),
                //new CultureInfo("en"),
                //new CultureInfo("en-US")
            };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fr-FR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            app.UseRequestLocalization(localizationOptions);
            app.UseElmah();
          app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            context.Database.Migrate();
            app.UseAuthentication();
            Seeding.SeedRoles(roleManager);
            Seeding.SeedUsers(userManager);
            Seeding.SeedData(context);
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public class MyTelemetryInitializer : ITelemetryInitializer
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public MyTelemetryInitializer(IServiceCollection services)
            {
                var serviceProvider = services.BuildServiceProvider();
                _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            }

            public void Initialize(ITelemetry telemetry)
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null)
                    return;
                if (context.User.Identity.IsAuthenticated)
                {
                    var userName = context.User.Identity.Name;

                    telemetry.Context.User.Id = userName;
                    telemetry.Context.User.AuthenticatedUserId = userName;
                }
            }
        }
    }
}

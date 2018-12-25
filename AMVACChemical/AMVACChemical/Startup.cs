using AMVACChemical.Entities;
using AMVACChemical.Entities.TrackAbout;
using AMVACChemical.Entities.TrackAbout.Identity;
using AMVACChemical.Interfaces.Repository;
using AMVACChemical.Repositories.TrackAbout;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AMVACChemical
{
    public class Startup
    {
        // Declare global variables
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        // create constructor
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"config.{_env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet-ER3.Web.App-716a052e-4235-4e5f-93bf-ba57ba6934b9");

            }

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            services.AddDbContext<AMVAC_TrackaboutContext>(
             options => options.UseSqlServer(_config["ConnectionStrings:LocalContextConnection"],b=> b.MigrationsAssembly("AMVACChemical"))
             );

            services.AddDbContext<AMVAC_IdentityContext>(
            options => options.UseSqlServer(_config["ConnectionStrings:IdentityContextConnection"], b => b.MigrationsAssembly("AMVACChemical"))
            );


            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                }
            });

            services.AddEntityFrameworkSqlServer().AddDbContext<AMVAC_TrackaboutContext>();
            services.AddEntityFrameworkSqlServer().AddDbContext<AMVAC_IdentityContext>();
            services.AddScoped<ITrackAboutRepository, TrackAboutRepository>();

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                // options => options.Cookies.applicationcookie.loginpath =""
                )
               
                .AddEntityFrameworkStores<AMVAC_IdentityContext>()
                .AddDefaultTokenProviders();
            // end

           // services.AddScoped<RoleManager<ApplicationRole>>();

            // set cookie when any attempt
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/TrackAbout/Login";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(8, 0, 0);
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                        ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            });

            // add logging:
            services.AddLogging();
            services.AddTransient<ApplicationRolesSeed>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory
            ,ApplicationRolesSeed roleSeed)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddFile("Logs/Trace-{Date}.txt", LogLevel.Trace);
            loggerFactory.AddFile("Logs/Error-{Date}.txt", LogLevel.Error);
        

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

            // integrate cors for cross integration
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            // Use of authorization
            app.UseAuthentication(); // The order of this Use method matters in relation to other pipeline middleware!!!
          
            app.UseIdentity();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "api/{controller}/{action}/{id?}");
            //});


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "TrackAbout", action = "Index" }
                    );
            });

            // role seeding in aspnetrole table
            roleSeed.Seed(_env, loggerFactory.CreateLogger(this.GetType().Name)).Wait();
        }
    }
}

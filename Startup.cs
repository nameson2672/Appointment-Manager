using AppoinmentScudeler.Models;
using AppoinmentScudeler.Services;
using AppointmentScheduling.DbInitializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AppoinmentScudeler
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
            services.AddDbContext<ApplicationDbContext>
                (x =>
                {

                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    string connStr;

                    if (env == "Development")
                    {
                        connStr = Configuration.GetConnectionString("DefaultConnection");


                    }
                    else
                    {
                        // Use connection string provided at runtime by Heroku.
                        var connUrl = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");

                        connUrl = connUrl.Replace("mysql://", string.Empty);
                        var userPassSide = connUrl.Split("@")[0];
                        var hostSide = connUrl.Split("@")[1];

                        var connUser = userPassSide.Split(":")[0];
                        var connPass = userPassSide.Split(":")[1];
                        var connHost = hostSide.Split("/")[0];
                        var connDb = hostSide.Split("/")[1].Split("?")[0];


                        connStr = $"server={connHost};Uid={connUser};Pwd={connPass};Database={connDb}";



                    }

                    x.UseSqlServer(connStr);

                });
            services.AddControllersWithViews();
            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddTransient<IAppoinmentServices, AppoinmentServices>();

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
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

            dbInitializer.Initalize();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

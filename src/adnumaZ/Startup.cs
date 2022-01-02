using System;
using adnumaZ.Data;
using adnumaZ.Data.Seeding;
using adnumaZ.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using adnumaZ.Services.UserService.Contracts;
using adnumaZ.Services.UserService;
using adnumaZ.Services.CommentService.Contracts;
using adnumaZ.Services.CommentService;
using BencodeNET.Parsing;
using adnumaZ.Services.TorrentService;
using adnumaZ.Services.TorrentService.Contracts;
using CloudinaryDotNet;

namespace adnumaZ
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
            if(sqlConnectionString == null)
            {
                sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(sqlConnectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ITorrentService, TorrentService>();
            services.AddTransient<IBencodeParser, BencodeParser>();

            services.AddDefaultIdentity<User>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddAntiforgery(options =>
            {
                //ASP.NET Core - Workshop - февруари 2020 3:58:03
                options.HeaderName = "X-CSRF-TOKEN"; //Add this so you can send the token with ajax !!!
            });

            services.Configure<CookiePolicyOptions>(
               options =>
               {
                   options.CheckConsentNeeded = context => true;
                   options.MinimumSameSitePolicy = SameSiteMode.None;
               });

            // Auto Mapper Configurations
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IConfiguration>(Configuration);

            // Cloudinary Configurations

            var cloudinaryCloudName = Configuration["Cloudinary:CloudName"];
            var cloudinaryKey = Configuration["Cloudinary:Key"];
            var cloudinarySecret = Configuration["Cloudinary:Secret"];


            Account cloudinaryAccount = new Account(
                    cloudinaryCloudName,
                    cloudinaryKey,
                    cloudinarySecret);

            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
            cloudinary.Api.Secure = true;

            services.AddSingleton(cloudinary);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder()
                     .SeedAsync(dbContext, serviceScope.ServiceProvider)
                     .GetAwaiter()
                     .GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithRedirects("/Error/HttpError?statusCode={0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "allTorrents",
                    pattern: "t/{id?}",
                    new { controller = "Torrent", action = "All" });
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}

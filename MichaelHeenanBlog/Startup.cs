using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MichaelHeenanBlog
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
            services.AddDbContext<BlogDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("MichaelHeenanBlog")));

            // Add Cookie Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => 
                {
                    o.LoginPath = "/SignIn";
                });

            // Add Authorisation options
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Administrator", policy =>
            //    {
            //        policy.Requirements.Add(new RolesAuthorizationRequirement(new[] { "Administrator" }));
            //    });
            //});

            // Add options to make Admin folder require authentication to access
            services.AddRazorPages()
                 .AddRazorPagesOptions(options =>
                 {
                     options.Conventions.AuthorizeAreaFolder("Admin", "/");
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Stackify Middleware
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            // Serilog Middleware
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication Middleware
            app.UseAuthentication();
           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LODM.highlights.Data;
using LODM.highlights.Models;
using LODM.highlights.Services;
using LODM.highlights.Services.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LODM.highlights
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
                
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddOptions();
            services.Configure<IConfigurationRoot>(Configuration);
            services.AddScoped<IHighlightService, YouTubeHighlightsService>();
            services.AddScoped<IMember, MemberService>();
            // Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "DestinyMappedRoute",
                    template: "destinyfoopoo",
                    defaults: new {controller = "Destiny", action = "Destiny"});

                routes.MapRoute(
                    name: "Bio_KingxMurphy",
                    template:"PlayerBio",
                    defaults: new { controller = "PlayerBio", action = "PlayerBio", selectedPlayerGamerTag = "KingxMurphy" });

                routes.MapRoute(
                    name: "Bio_Take2Chance",
                    template: "PlayerBio",
                    defaults: new { controller = "PlayerBio", action = "PlayerBio", selectedPlayerGamerTag = "Take2Chance" });

                routes.MapRoute(
                    name: "Bio_TheYungJacques",
                    template: "PlayerBio",
                    defaults: new { controller = "PlayerBio", action = "PlayerBio", selectedPlayerGamerTag = "TheYungJacques" });

                routes.MapRoute(
                    name: "Bio_JimmyPotato",
                    template: "PlayerBio",
                    defaults: new { controller = "PlayerBio", action = "PlayerBio", selectedPlayerGamerTag = "JimmyPotato" });

                routes.MapRoute(
                    name: "Bio_Syphin",
                    template: "PlayerBio",
                    defaults: new { controller = "PlayerBio", action = "PlayerBio", selectedPlayerGamerTag = "Syphin" });

                routes.MapRoute(
                    name: "Bio_Headsho7",
                    template: "PlayerBio",
                    defaults: new { controller = "PlayerBio", action = "PlayerBio", selectedPlayerGamerTag = "Headsho7" });
            });
        }
    }
}

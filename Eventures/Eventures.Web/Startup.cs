namespace Eventures.Web
{
    using System;

    using AutoMapper;

    using Eventures.Data;
    using Eventures.Models;
    using Eventures.Web.Filters;
    using Eventures.Web.MiddleWares.Extensions;
    using Eventures.Web.Services;
    using Eventures.Web.Services.Contracts;
    using Eventures.Web.ViewModels.Account;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IServiceProvider provider,
            ILoggerFactory loggerFactory)
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

            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile($"Logs/Events.txt");

            app.UseSeedDataMiddleWare();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(
                routes => { routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddDbContext<EventuresDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<EventuresUser, IdentityRole>(
                opt =>
                    {
                        opt.SignIn.RequireConfirmedEmail = false;
                        opt.Password.RequireLowercase = false;
                        opt.Password.RequireUppercase = false;
                        opt.Password.RequireNonAlphanumeric = false;
                        opt.Password.RequireDigit = false;
                        opt.Password.RequiredUniqueChars = 0;
                        opt.Password.RequiredLength = 3;
                    }).AddDefaultTokenProviders().AddEntityFrameworkStores<EventuresDbContext>();

            services.AddAuthentication().AddFacebook(
                options =>
                    {
                        options.AppId = this.Configuration["Authentication:Facebook:AppId"];
                        options.AppSecret = this.Configuration["Authentication:Facebook:AppSecret"];
                    });

            services.AddAutoMapper();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<EventsLogActionFilter>();

            services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}
namespace Eventures.Web.MiddleWares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Eventures.Data;
    using Eventures.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager, IServiceProvider provider)
        {
            var dbContext = provider.GetService<EventuresDbContext>();

            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(roleManager);
            }

            if (!dbContext.Events.Any())
            {
                await this.SeedEvents(dbContext);
            }

            await this.next(context);
        }

        private async Task SeedEvents(EventuresDbContext context)
        {
            var sampleEvents = new List<EventuresEvent>();
            for (var i = 0; i < 10; i++)
            {
                var sampleEvent = new EventuresEvent
                                      {
                                          Name = $"Event number {i}",
                                          Place = $"Place number {i}",
                                          Start = DateTime.Now.AddDays(i),
                                          End = DateTime.Now.AddMonths(i),
                                          PricePerTicket = 10 * i,
                                          TotalTickets = 1000 * i
                                      };

                sampleEvents.Add(sampleEvent);
            }

            await context.Events.AddRangeAsync(sampleEvents);
            await context.SaveChangesAsync();
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}
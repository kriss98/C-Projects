namespace Eventures.Data
{
    using Eventures.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EventuresDbContext : IdentityDbContext<EventuresUser>
    {
        public EventuresDbContext()
        {
        }

        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventuresEvent> Events { get; set; }

        public DbSet<EventuresOrder> Orders { get; set; }
    }
}
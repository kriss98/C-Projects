namespace MishMash.Data
{
    using Microsoft.EntityFrameworkCore;

    using MishMash.Models;

    public class MishMashContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserChannel> UsersChannels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-MQHNDKS\\SQLEXPRESS;Database=MishMash;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.FollowedChannels).WithOne(fc => fc.User)
                .HasForeignKey(fc => fc.UserId);

            modelBuilder.Entity<Channel>().HasMany(ch => ch.Followers).WithOne(f => f.Channel)
                .HasForeignKey(f => f.ChannelId);

            modelBuilder.Entity<UserChannel>().HasKey(uc => new { uc.ChannelId, uc.UserId });
        }
    }
}
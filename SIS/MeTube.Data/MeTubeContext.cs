namespace MeTube.Data
{
    using MeTube.Models;

    using Microsoft.EntityFrameworkCore;
    public class MeTubeContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-MQHNDKS\\SQLEXPRESS;Database=MeTube;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tube>().HasOne(t => t.Uploader).WithMany(u => u.Tubes).HasForeignKey(t => t.UploaderId);
        }
    }
}
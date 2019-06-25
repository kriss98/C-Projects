namespace Chushka.Data
{
    using Chushka.Models;

    using Microsoft.EntityFrameworkCore;

    public class ChushkaContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-MQHNDKS\\SQLEXPRESS;Database=Chushka;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(o => o.Client).WithMany(c => c.Orders).HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Order>().HasOne(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
        }
    }
}
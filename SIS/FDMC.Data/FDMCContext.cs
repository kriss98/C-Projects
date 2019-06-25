namespace FDMC.Data
{
    using FDMC.Models;

    using Microsoft.EntityFrameworkCore;

    public class FDMCContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Kitten> Kittens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-MQHNDKS\\SQLEXPRESS;Database=FDMC;Integrated Security=True");
        }
    }
}
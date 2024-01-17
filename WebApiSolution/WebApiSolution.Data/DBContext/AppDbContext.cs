using Microsoft.EntityFrameworkCore;
using WebApiSolution.Domain.Models;

namespace WebApiSolution.Data.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                  .Property(c => c.Id)
                  .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>()
           .Property(o => o.Price)
           .HasPrecision(18, 2); //
            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}

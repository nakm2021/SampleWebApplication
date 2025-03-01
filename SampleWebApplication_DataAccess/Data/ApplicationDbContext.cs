using Microsoft.EntityFrameworkCore;
using SampleWebApplication_Models;

namespace SampleWebApplication_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .ToTable("Order");
            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem")
                .HasKey(oi => new { oi.OrderId, oi.ProductId });
        }
    }
}

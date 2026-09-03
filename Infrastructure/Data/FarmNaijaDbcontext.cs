using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FarmNaijaDbcontext : DbContext
    {
        public FarmNaijaDbcontext(DbContextOptions<FarmNaijaDbcontext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Farmer> Farmers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(FarmNaijaDbcontext).Assembly);
        }
    }
}
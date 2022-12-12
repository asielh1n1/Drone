using DroneCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DroneTest.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }

        public DbSet<Drone> Drone { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<DeliveryDetail> DeliveryDetail { get; set; }

    }
}

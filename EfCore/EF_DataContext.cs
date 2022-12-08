using Microsoft.EntityFrameworkCore;
using ShipmentAPI.Model;

namespace ShipmentAPI.EfCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Shipment>? Shipments { get; set; }
        public DbSet<Carrier>? Carrier { get; set; }
        public DbSet<CarrierService>? CarrierService { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Carrier>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<CarrierService>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<User>().HasIndex(x => x.Email).IsUnique();



            builder.Entity<Carrier>(entity =>
{
    entity.Property(e => e.Name).IsUnicode();
});
        }
    }
}
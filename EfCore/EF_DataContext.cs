using Microsoft.EntityFrameworkCore;
using ShipmentApi.Model;

namespace ShipmentApi.EfCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CarrierService>().HasIndex(u => u.name).IsUnique();

        }
    }
}
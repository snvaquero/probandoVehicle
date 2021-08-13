using Microsoft.EntityFrameworkCore;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasIndex(dt => dt.Description).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(dt => dt.Description).IsUnique();
            modelBuilder.Entity<VehicleType>().HasIndex(dt => dt.Description).IsUnique();
        }
    }
}

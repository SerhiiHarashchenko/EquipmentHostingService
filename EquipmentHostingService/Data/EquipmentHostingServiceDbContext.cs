using EquipmentHostingService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentHostingService.Data
{
    public class EquipmentHostingServiceDbContext : DbContext
    {
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }

        public EquipmentHostingServiceDbContext(DbContextOptions<EquipmentHostingServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentPlacementContract>()
                .HasIndex(e => e.FacilityCode)
                .HasDatabaseName("IX_EquipmentPlacementContract_FacilityCode");

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Infrastructure.Data.ViewModels;

namespace TechReserveSystem.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentReservation> EquipmentReservations { get; set; }
        public DbSet<UserReservationViewModel> UserReservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<EquipmentCategory>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Equipment>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<UserReservationViewModel>()
                .ToView("UserReservations")
                .HasNoKey();
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
using LetMePark.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LetMePark.Infrastructure.DAL
{
    internal sealed class LetMeParkDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }
        public DbSet<User> Users { get; set; }

        
        public LetMeParkDbContext(DbContextOptions<LetMeParkDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

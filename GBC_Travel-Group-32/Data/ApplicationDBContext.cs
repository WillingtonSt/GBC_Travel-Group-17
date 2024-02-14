using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_32.Models;

namespace GBC_Travel_Group_32.Data {
    public class ApplicationDBContext : DbContext {


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {

           
        }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<CarRental> CarRentals { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Listing>().ToTable("Listings");
            modelBuilder.Entity<Flight>().ToTable("Flights");
            modelBuilder.Entity<Hotel>().ToTable("Hotels");
            modelBuilder.Entity<CarRental>().ToTable("CarRentals");
        }

    }
}

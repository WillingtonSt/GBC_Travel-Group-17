using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GBC_Travel_Group_32.Data {
    public class ApplicationDBContext : IdentityDbContext<User> {

        public ApplicationDBContext() : base() {



        }
    


   



    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {

          
        }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<CarRental> CarRentals { get; set;}

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<ListingReview> ListingReviews { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

         base.OnModelCreating(modelBuilder);
     

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);

            modelBuilder.Entity<Listing>().ToTable("Listings");
            modelBuilder.Entity<Flight>().ToTable("Flights");
            modelBuilder.Entity<Hotel>().ToTable("Hotels");
            modelBuilder.Entity<CarRental>().ToTable("CarRentals");
            modelBuilder.Entity<Booking>().ToTable("Bookings");


        }

    }
}

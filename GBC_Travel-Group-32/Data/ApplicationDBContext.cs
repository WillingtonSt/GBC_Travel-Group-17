using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_32.Models;

namespace GBC_Travel_Group_32.Data {
    public class ApplicationDBContext : DbContext {


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {



           
        }


        public DbSet<Listing> Listings { get; set; }


    }
}

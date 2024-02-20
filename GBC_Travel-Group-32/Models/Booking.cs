using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_32.Models {
    public class Booking {


        [Key] 
        public int BookingId { get; set; }

        public int ListingId { get; set; }

        public string? UserId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        public DateTime? BookingDate { get; set; }

        public DateTime BookingEndDate { get; set; }

        public Listing? listing { get; set; }


    }
}

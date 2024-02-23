
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_32.Models {
    public class Listing {

        [Key]
        public int ListingId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, float.MaxValue, ErrorMessage = "Price cannot be negative")]
        public float Price { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string? UserId {  get; set; }

        public bool Available { get; set; }
        

     
    }


    public class Flight : Listing {

        [DataType(DataType.Date)]
        [Required]
        public DateTime FlightDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Passenger Maximum must be at least 1")]
        public int MaxPassengers { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public string? Destination { get; set; }

     


    }


    public class Hotel : Listing {

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartPeriod { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndPeriod { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Rooms cannot be less than 0")]
        public int Rooms {  get; set; }

     

    }

    public class CarRental : Listing {

        [Required]
        public string? Manufacturer { get; set; }

        [Required]
        public string? Model { get; set; }

      
    }
}


using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_32.Models {
    public class Listing {

        [Key]
        public int ListingId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public float Price { get; set; }

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

        public int Rooms {  get; set; }

     

    }

    public class CarRental : Listing {

        [Required]
        public string? Manufacturer { get; set; }

        [Required]
        public string? Model { get; set; }

      
    }
}
